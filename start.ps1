
Write-Host "###########################################################################"
Write-Host "      __/\\\\\\\\\\\\\____/\\\\\\\\\\\\\\\_____/\\\\\\\\\\\\_        "
Write-Host "       _\/\\\/////////\\\_\///////\\\/////____/\\\//////////__       "
Write-Host "        _\/\\\_______\/\\\_______\/\\\________/\\\_____________      "
Write-Host "         _\/\\\\\\\\\\\\\\________\/\\\_______\/\\\____/\\\\\\\_     "
Write-Host "          _\/\\\/////////\\\_______\/\\\_______\/\\\___\/////\\\_    "
Write-Host "           _\/\\\_______\/\\\_______\/\\\_______\/\\\_______\/\\\_   "
Write-Host "            _\/\\\_______\/\\\_______\/\\\_______\/\\\_______\/\\\_  "
Write-Host "             _\/\\\\\\\\\\\\\/________\/\\\_______\//\\\\\\\\\\\\/__ "
Write-Host "              _\/////////////__________\///_________\////////////____"
Write-Host "###########################################################################"
Write-Host "Starting the Better Than Google project"
Write-Host "Please ensure that port 1433 is not in use on your local machine"
Write-Host "If you are using a firewall, please ensure that port 1433 is not blocked"
Write-Host "###########################################################################"

$username = "sa"
$password = "f6278528-d366-49ba-bcfb-1f1865e7a5d1" # New-Guid
$database = "btg"
$dbhost = "localhost"
$connectionString = "Server=$dbhost;Database=$database;User Id=$username;Password=$password;Trusted_Connection=False;Encrypt=False"

Write-Host -NoNewline "Initializing user secrets and setting database connection string"
dotnet user-secrets init --project .\WebService.Core\Server > $null
dotnet user-secrets set "ConnectionStrings:$database" "$connectionString" --project WebService.Core/Server > $null
Write-Host "Done."

Write-Host -NoNewline "Pulling the mssql image from Docker Hub.. "
docker pull mcr.microsoft.com/mssql/server:2019-latest > $null
Write-Host "Done."

Write-Host -NoNewline "Starting the DB container.. "
docker kill bdsa2021_btg_db > $null
docker rm bdsa2021_btg_db > $null
docker run --name bdsa2021_btg_db -d -ti -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 mcr.microsoft.com/mssql/server:2019-latest
Write-Host "Done."

Write-Host "Sleeping for 30 seconds to allow the DB to start"
Start-Sleep -Seconds 30

Write-Host "Updating the database.."
dotnet ef database update -s WebService.Core\Server --project .\WebService.Entities\ > $null
Write-Host "Done."

Write-Host "Starting background job to launch your web browser when the web server is online, but might not be seeding the database when launching."
Write-Host "Please monitor the terminal window to see if the seeding is still ongoing."
Start-Job -FilePath .\open_delayed_browser.ps1

Write-Host "Starting the server"
dotnet run --project .\WebService.Core\Server

Write-Host -NoNewline "Stopping the DB container.. "
docker kill bdsa2021_btg_db > $null
docker rm bdsa2021_btg_db > $null
Write-Host "Done."

