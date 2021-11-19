.PHONY: run
run:
	dotnet run --project WebService.Core/Client

.PHONY: run-watched
run-watched:
	dotnet watch run --project WebService.Core/Client

.PHONY: test
test:
	dotnet test
