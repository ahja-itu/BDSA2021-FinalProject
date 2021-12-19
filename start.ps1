
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
$password = New-Guid
$database = "btg"
$dbhost = "localhost"
$connectionString = "Server=$dbhost;Database=$database;User Id=$username;Password=$password;Trusted_Connection=False;Encrypt=False"

Write-Host "Initializing user secrets and setting database connection string"
dotnet user-secrets init --project .\WebService.Core\Server
dotnet user-secrets set "ConnectionStrings:$database" "$connectionString" --project WebService.Core/Server

Write-Host "Pulling the mssql image from Docker Hub"
docker pull mcr.microsoft.com/mssql/server:2019-latest

Write-Host "Starting the DB container"
docker kill bdsa2021_btg_db
docker rm bdsa2021_btg_db
docker run --name bdsa2021_btg_db -d -ti -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 mcr.microsoft.com/mssql/server:2019-latest

Write-Host "Sleeping for 30 seconds to allow the DB to start"
Start-Sleep -Seconds 30

Write-Host "Updating the database"
dotnet ef database update -s WebService.Core\Server --project .\WebService.Entities\

Write-Host "Starting the server"
dotnet run --project .\WebService.Core\Server

Write-Host "Stopping the DB container"
docker kill bdsa2021_btg_db
docker rm bdsa2021_btg_db

