Get-ChildItem -Path ".\All\**\*.puml" -Recurse
	| Where-Object { $_.Name -notlike "*Using*" }  
	| Where-Object { $_.Name -notlike "*Info*" } 
	| Where-Object { $_.Name -notlike "*Test*" } 
	| Where-Object { $_.Name -notlike "*Assembly*" } 
	| Where-Object { $_.Name -notlike "*cshtml*" } 
	| Where-Object { $_.Name -notlike "*Program*" } 
	| Where-Object { $_.Directory -notlike "*Migrations*" }
	| Where-Object { $_.Name -notlike "include*" }
	| foreach { Move-Item $_ .\CollectedPumls }
