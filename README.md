# RESTAPI Smartcity project - Bepway
## Structure du projet
```
.root
├── API
│   ├── Controllers 
│   │   ├── APIController.cs : ControllerBase
│   │   ├── CompanyController.cs : APIController.cs
│   │   ├── JwtController.cs
│   │   └── UserController.cs : APIController.cs
│   ├── Infrastructure
│   │   ├── BusinnessExceptionFilter : IExceptionFilter
│   │   ├── ConfigurationHelper.cs
│   │   ├── JwtIssuerOptions.cs
│   │   ├── MappingProfile.cs
│   │   └── PrivateClaims.cs
│   ├── API.csproj
│   ├── appsettings.json
│   ├── Program.cs
│   ├── secrets.json
│   └── Startup.cs
├── DAL
│   ├── BepwayContext.cs : DbContext
│   ├── CompanyDataAccess.cs
│   ├── ConnectionHelper.cs
│   ├── DAL.csproj
│   ├── DataAccess.cs
│   ├── secrets.json
│   └── UserDataAccess.cs
├── DTO
│   ├── ActivitySector.cs
│   ├── BusinessError.cs
│   ├── Company.cs
│   ├── DTO.csproj
│   ├── LoginModel.cs
│   └── User.cs
├── Model
│   ├── ActivitySector.cs
│   ├── BusinessException.cs
│   ├── Company.cs
│   ├── Constants.cs
│   ├── Model.csproj
│   └── User.cs
└── Tests
   ├── Tests.cs
   └── Tests.csproj
```