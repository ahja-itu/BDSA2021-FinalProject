.PHONY: run
run:
	dotnet run --project WebService.Core

.PHONY: run-watched
run-watched:
	dotnet watch run --project WebService.Core

.PHONY: test
test:
	dotnet test

.PHONY: build
build:
	dotnet build --project WebService.Core
