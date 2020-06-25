# Cubes3DIntersection
Cubes3DIntersection is a web application to check if two 3D cubes collide and, eventually, calculate the intersected volume. It is built following the requirements: https://documentcloud.adobe.com/link/track?uri=urn:aaid:scds:US:e0a740b7-d870-4727-87cc-fb9815734b15.

Tests are automatically verified: 
![Build and Test](https://github.com/dannyc84/Cubes3DIntersection/workflows/Build%20and%20Test/badge.svg)

## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
* [.Net Core 3.1 or later](https://dotnet.microsoft.com/download/dotnet-core/3.1)
* [EF Core 3.1 or later](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore)

### Installing
Follow these steps to get your development environment set up:
1. Clone the repository
2. At the root directory, restore required packages by running:
```csharp
dotnet restore
```
3. Next, build the solution by running:
```csharp
dotnet build
```
4. Next, within the Cubes3DIntersection.Api directory, launch the back end by running:
```csharp
dotnet run
```
5. Launch http://localhost:44340/ in your browser to view the Swagger UI documentation.

If you have **Visual Studio** after cloning, click on Open solution with your IDE and Cubes3DIntersection.Api should be the start-up project. Directly run this project on Visual Studio with **F5 or Ctrl+F5**. You will see index page of project, you can navigate product and category pages and you can perform crud operations on your browser.

### Usage
The project uses a persistent database, even if the default configuration of Entity Framework Database is **"InMemoryDatabase"**.
After cloning or downloading the solution, you will need to run its Entity Framework Core **migrations** before you will be able to run the app. You can see the ConfigureDatabases method in **Startup.cs** (see below).  

```csharp
public void ConfigureDatabases(IServiceCollection services)
{
    // use in-memory database
    //services.AddDbContext<Cube3DIntersectionDbContext>(c =>
    //    c.UseInMemoryDatabase("Cubes3DIntersectionConnection")
    //    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

    // use real database
    services.AddDbContext<Cubes3DIntersectionDbContext>(c =>
        c.UseSqlServer(Configuration.GetConnectionString("Cubes3DIntersectionConnection"))
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
}
```

1. Ensure your connection strings in ```appsettings.json``` point to your local SQL Server instance.

2. Open a command prompt in the Web folder and execute the following commands:

```csharp
dotnet restore
dotnet ef database update -c Cube3DIntersectionDbContext -p ../Cubes3DIntersection.Infrastructure/Cubes3DIntersection.Infrastructure.csproj -s Cubes3DIntersection.Api.csproj
```
Or you can direct call ef commands from Visual Studio **Package Manager Console**. Open Package Manager Console, set default project to Cubes3DIntersection.Infrastructure and run below command;
```csharp
update-database
```
These commands will create Cubes3DIntersection database. You can see from **Cube3DIntersectionDbContext.cs**.
1. Run the application.
The first time you run the application, it will seed Cubes3DIntersection sql server database.

If you modify-change or add new some of entities to Core project, you should run ef migrate commands in order to update your database as the same way but below commands;
```csharp
add migration YourCustomEntityChanges
update-database
```

## Layered Architecture
Cubes3DIntersection implements NLayer **Hexagonal architecture** (Core, Application, Infrastructure and Presentation Layers) and **Domain Driven Design** (Entities, Repositories, Domain/Application Services, DTO's...). Also implements and provides a good infrastructure to implement **best practices** such as Dependency Injection, logging, validation, exception handling and so on.
Aimed to be a **Clean Architecture** also called **Onion Architecture**, with applying **SOLID principles**. Also implements and provides a good infrastructure to implement **best practices** like **loosely-coupled, dependency-inverted** architecture

### Structure of Project
Cubes3DIntersection include layers divided by **4 project**;
* Core
    * Entities    
    * Interfaces
    * ValueObjects
    * Exceptions
* Application    
    * Interfaces    
    * Services
    * Models (DTOs)
    * Mappers
    * Exceptions
    * Extensions
    * Factories
* Infrastructure
    * Data
    * Repository
    * Services
    * Migrations
    * Logging
    * Exceptions
* Api
    * Controllers
    * Views

#### Interfaces
Abstraction of a Generic Repository (IGenericRepository) for CRUD operations on Domain objects. This interface include database operations without any application and ui responsibilities.

### Infrastructure Layer
Implementation of Core interfaces in this project with **Entity Framework Core** and other dependencies.
Most of your application's dependence on external resources should be implemented in classes defined in the Infrastructure project. These classes must implement the interfaces defined in Core. If you have a very large project with many dependencies, it may make sense to have more than one Infrastructure project (eg Infrastructure.Data), but in most projects one Infrastructure project that contains folders works well.
This could be includes, for example, **e-mail providers, file access, web api clients**, etc. For now this repository only dependend sample data access and basic domain actions, by this way there will be no direct links to your Core or UI projects.

#### Data
Includes **Entity Framework Core Context** and tables in this folder. When new entity created, it should add to context and configure in context.
The Infrastructure project depends on Microsoft.**EntityFrameworkCore.SqlServer** and EF.Core related nuget packages, you can check nuget packages of Infrastructure layer. If you want to change your data access layer, it can easily be replaced with a lighter-weight ORM like Dapper. 

#### Migrations
EF add-migration classes.

#### Repository
EF Repository implementation. This class is responsible for the database operations

#### Services
Custom services implementation, like email, cron jobs etc.

### Application Layer
Development of **Domain Logic with implementation**. Interfaces drives business requirements and implementations in this layer.
In this layer we can add validation , authorization, logging, exception handling etc. -- cross cutting activities should be handled in here.

### Api Layer
Development of Api Logic with implementation. Interfaces drives business requirements and implementations in this layer.
The application's main **starting point** is the ASP.NET Core web project. This is a classical console application, with a public static void Main method in Program.cs. It currently uses the default **ASP.NET Core project template** which based on **Razor Pages** templates. This includes appsettings.json file plus environment variables in order to stored configuration parameters, and is configured in Startup.cs.

### Test Layer
For each layer, there should be a test project which includes intended layer dependencies and mock classes. So that means Core-Application-Infrastructure and Api layer has their own test layer. By this way this test projects also divided by **unit, functional and integration tests** defined by in which layer it is implemented. 
Test projects using **xunit and Mock libraries**.  xunit, because that's what ASP.NET Core uses internally to test the product. Moq, because perform to create fake objects clearly and its very modular.
Due to time constraints, instead of unit tests for all the projects, some tests made with **postman** have been added.

## Technologies
* .NET Core 3.1
* ASP.NET Core 3.1
* Entity Framework Core 3.1 
* .NET Core Native DI
* AutoMapper

## Architecture
* Clean Architecture
* Full architecture with responsibility separation of concerns
* SOLID and Clean Code
* Domain Driven Design (Layers and Domain Model Pattern)
* Unit of Work
* Repository and Generic Repository
* Monolitic Deployment Architecture

## Disclaimer

* This repository is not intended to be a definitive solution.
* This repository not implemented a lot of 3rd party packages, we are try to avoid the over engineering when building on best practices.
* Beware to use in production way.

## Contributors

Thanks to the following people who have contributed to this project:

* [@dannyc84](https://github.com/dannyc84)

## Contact

If you want to contact me you can reach me at daniele.crivello84@libero.it.

## Code of Conduct

This project uses the following code of conduct: [CODE_OF_CONDUCT](CODE_OF_CONDUCT.md).

## License

This project uses the following license: [MIT](LICENSE.md).
