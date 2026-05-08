# Inventory API

RESTful API developed with .NET 8 and Entity Framework Core.

---

# 🚀 Technologies

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server LocalDB
- FluentValidation
- AutoMapper
- Serilog
- xUnit

---

# 📦 Architecture

The project follows a layered architecture:

- API
- Application
- Domain
- Infrastructure
- Shared
- Tests

---

# ▶️ How to Run

## Requirements

- Visual Studio 2022
- .NET 8 SDK
- SQL Server LocalDB

## Steps

1. Clone repository

2. Open solution in Visual Studio

3. Set `InventoryApi.API` as Startup Project

4. Run project

The database will be automatically created and migrations applied on startup.

---

# 📌 Features

- Full CRUD operations
- DTO pattern
- Repository Pattern
- AutoMapper
- FluentValidation
- Global exception handling
- Serilog logging
- Swagger documentation

---

# 🧪 Tests Performed

Unit tests were implemented for:

- Product creation
- Data persistence using InMemory database

Run tests with:

```powershell
dotnet test
```

---

# 📚 Swagger

Swagger UI available at:

```text
https://localhost:{port}/swagger
```