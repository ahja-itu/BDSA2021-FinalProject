# BDSA2021-FinalProject
This is the final project for the BDSA2021 course at ITU


## Test user login


Email: test@itubdsabridgethegap.onmicrosoft.com
Password: 42_ER_1_TAL

## Running the database

This will generate a new connection string and store it as a user secret. Then it will run a Postgres DBMS docker container.

TODO: How to properly initialize the DB with perhaps seed data..

```pwsh
.\start_db.ps1
```


## Making migrations

```pwsh
.\migrate.ps1 NAME
``` 

## Updating the database

```pwsh
.\update_db.ps1
```