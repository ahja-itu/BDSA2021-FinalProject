
$username = "postgres"
$password = "postgres" # New-Guid
$database = "btg"
$host = "localhost:55432"
$connectionString = "Host=$host;Database=$database;Username=$username;Password=$password"

dotnet user-secrets set "ConnectionStrings:$database" "$connectionString" --project WebService.Core/Server

docker run -ti --rm -p 55432:5432 --name bdsa-btg-db -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=$password -e POSTGRES_DB=$database postgres