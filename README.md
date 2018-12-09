# RESTAPI Smartcity project - Bepway

## Agile Method
| Todo | Doing | Done | Rejected |
| :--- | :--- | :--- | :--- |
| le contexte et les modèles de la DB en CodeFirst
les DTO et exceptions
l'authentification
l'automapping
l'externalisation de la connectionstring
toute la logique
les controllers
la documentation en ligne Swagger/OpenAPI


## Structure du projet
```
.root
├── API (webapi)
│   ├── Controllers 
│   │   ├── APIController.cs : ControllerBase
│   │   └── ...
│   ├── Infrastructure
│   │   ├── AuthentificationRepository.cs ?
│   │   ├── Profile.cs
│   │   ├── JwtIssuerOptions.cs
│   │   └── BusinnessExceptionFilter : IExceptionFilter
│   ├── appsettings.json
│   │   └── ConnectionString
│   └── API.csproj
│       ├── PackageReference : AutoMapper
│       ├── PackageReference : AutoMapper.Extensions.Microsoft.DependencyInjection
│       ├── PackageReference : Swashbuckle.AspNetCore
│       ├── ProjectReference : DTO.csproj
│       ├── ProjectReference : DAL.csproj
│       └── ProjectReference : Model.csproj
├── DTO (classlib -f netcoreapp2.1)
│   └── ...
├── Model (classlib -f netcoreapp2.1)
│   └── ...
├── DAL (classlib -f netcoreapp2.1)
│   ├── appsettings.json
│   │   └── ConnectionString
│   ├── DAL.csproj
│   │   ├── PackageReference : Microsoft.EntityFrameworkCore
│   │   ├── PackageReference : Microsoft.EntityFrameworkCore.Design
│   │   ├── PackageReference : Microsoft.EntityFrameworkCore.SqlServer
│   │   ├── PackageReference : Microsoft.Extensions.Configuration
│   │   ├── PackageReference : Microsoft.Extensions.Configuration.Binder
│   │   ├── PackageReference : Microsoft.Extensions.Configuration.FileExtensions
│   │   ├── PackageReference : Microsoft.Extensions.Configuration.Json
│   │   └── ProjectReference : Model.csproj
│   └── ...
└── Tests (mstest) [optional]
    └── DALTests.cs
```
