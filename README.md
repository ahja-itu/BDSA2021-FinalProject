# BDSA2021-FinalProject

[![.NET](https://github.com/andreaswachs/BDSA2021-FinalProject/actions/workflows/unit_tests_on_push.yml/badge.svg)](https://github.com/andreaswachs/BDSA2021-FinalProject/actions/workflows/unit_tests_on_push.yml)

This is the final project for the BDSA2021 course at ITU.

The vertical slice of this project is to provide the ability for users to search for software engineering training material. The main feature of this project is the search functionality that matches the users search input to the material contained in the database.

## Prerequisites and system requirements

In order to run this project, you need to have the following pieces of software installed, working and running:

- Docker
- DotNET 6.0
- Entity Framework Core tooling
- Powershell

This project has only been tested to work completely, mostly when also considering the PowerShell scripts, on Windows and you are adviced to run this off Windows too.

## Project publications

- To view the latest published code coverage report click [here](https://andreaswachs.github.io/BDSA2021-FinalProject/Documentation/CodeCoverageReport.html)
- To view the latest published code metrics analysis click [here](https://andreaswachs.github.io/BDSA2021-FinalProject/Documentation/CodeMetrics.xlsx)

## Run the project

To run the project, run the `start.ps1` file:

```pwsh
.\start.ps1
```

There is some startup time with this command, as the database and web server needs to be started synchonously.

Note: as a service to you, the start script will open your browser when the server comes online. Although it launches a little early, and you need to wait to refresh when the server is done seeding the database. 

Note II: should your browser not open due to various reasons, please click [this](https://localhost:7213/) link when the server is running.

## Development

### Test user login

Email: test@itubdsabridgethegap.onmicrosoft.com
Password: 42_ER_1_TAL

### Running the database

This will generate a new connection string and store it as a user secret. Then it will run a Postgres DBMS docker container.

```pwsh
.\start_db.ps1
```

### Making migrations

```pwsh
.\migrate.ps1 NAME
``` 

### Updating the database

```pwsh
.\update_db.ps1
```

### Running the web server

This requires the database to be online and the update script to have been ran.

```pwsh
dotnet run --project .\WebService.Core\Server
```

### Using Swagger to view the API

While the server is running (in development mode), then you can access [https://localhost:7213/swagger/](https://localhost:7213/swagger/) to view the API specification exposed by the web server.

## Documentation

This project leverages the [PlantUmlClassDiagramGenerator](https://github.com/pierre3/PlantUmlClassDiagramGenerator) library to create PlantUML diagrams from C# source files.
It is currently not fully compatible with C# 8+, so some diagrams are not correctly generated, so you will need to fix some files after a convertion.

To be able to work with PlantUML files, you need to install the PlantUML jar file to your system and various dependencies. You can read a guide on installing PlantUML on your local system [here](https://plantuml.com/starting).

To work with the documentation, you need to change directory from the root folder to `.\Documentation\UML`.


### Create PUML files

```pwsh
.\create_pumls.ps1
```

### Pull the PUML files from the autogenerated structure into the `CollectedPumls` folder

```pwsh
.\pull_pumls.ps1
```

### Convert PUML files to PNG images and move them into the `Images` folder

```pwsh
.\convert.ps1
```

### Produce code coverage report

We've used the Rider IDE from Jetbrains in order to create customized code coverage reports that ignore test projects and classes that were provided by the Blazor framework.

To produce a code coverage report in Rider:

- Press Tests -> Cover Unit Tests and allow for tests to finish
- In the window that pops up to the right, then press the "Export Coverage Report" -> Export to HTML
- Save as "CodeCoverageReport" in the Documentation library