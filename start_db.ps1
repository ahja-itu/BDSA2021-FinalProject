
$username = "sa"
$password = "0EB05C29-F193-472F-99CC-C538BF6EC1C6"
$database = "btg"
$dbhost = "localhost"
# $dbport = "55432"
# $connectionString = "Host=$dbhost;Port=$dbport;Database=$database;Username=$username;Password=$password"

$connectionString = "Server=$dbhost;Database=$database;User Id=$username;Password=$password;Trusted_Connection=False;Encrypt=False"
dotnet user-secrets set "ConnectionStrings:$database" "$connectionString" --project WebService.Core/Server

# docker run -ti --rm -p 55432:5432 --name bdsa-btg-db -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=$password -e POSTGRES_DB=$database postgres


docker pull mcr.microsoft.com/mssql/server:2019-latest
# $password = New-Guid
docker run -ti -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 mcr.microsoft.com/mssql/server:2019-latest
# $database = "Futurama"
