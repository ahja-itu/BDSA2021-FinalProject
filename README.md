# BDSA2021-FinalProject
This is the final project for the BDSA2021 course at ITU

## Prerequisites

In order to run this project, you need to have the following pieces of software installed, working and running:

- Docker
- dotNET Core 6.0
- Powershell

## Run the project

To run the project, run the `start.ps1` file:

```pwsh
.\start.ps1
```

It takes just a few minutes for the system to be fully running. After asking docker to start the DB container, the script waits 30 seconds. After that the server starts up and populates the DB, which takes a bit of time.

## Development

### Test user login

Email: test@itubdsabridgethegap.onmicrosoft.com
Password: 42_ER_1_TAL

### Running the database

This will generate a new connection string and store it as a user secret. Then it will run a Postgres DBMS docker container.

TODO: How to properly initialize the DB with perhaps seed data..

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