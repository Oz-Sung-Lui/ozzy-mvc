# ozzy-mvc

## Introduction
This project is an assignment in Software Studio Class @CE KMITL created by ASP.NET MVC. 

## Database Migration
Our database is just SQLite. We've to migrate our DB before running our project.

```sh
dotnet ef migrations add InitialCreate
dotnet ef database update
```

If you cannot run these commands, you've to install dotnet ef tool first.

```sh
dotnet tool install --global dotnet-ef
```

## Running our project
```sh
dotnet watch run
```
