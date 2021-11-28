if ($args.count -eq 0) {
	Write-Host "Missing migration name (should also be non-empty)"
}
else {
	$migrationName = $args[0]
	if ($migrationName -eq $null) {
		Write-Host "Argument passed should not be empty"
	}
	else {
		dotnet ef migrations add $migrationName -s WebService.Core\Server --project .\WebService.Entities\
	}
}