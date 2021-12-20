Get-ChildItem ".\CollectedPumls\*.puml" | Foreach-Object { plantuml.cmd $_ }

Get-ChildItem ".\CollectedPumls\*.png" | Foreach-Object { Move-Item $_ .\Images -Force }



