# Backend (.NET API)

ASP.NET Core **Web API** on **.NET 8**, **SQL Server** via **EF Core**, Swagger in Development. Two paginated cliente endpoints arrive in subsequent phases (**stored procedure** + **LINQ**).

## Database (SQL Server)

Scripts in [`db/`](db/): run `001` → `002` → `003` on database `EvaluaClientes` (or update the connection string).

## Running the API locally

Requires [.NET 8 SDK](https://dotnet.microsoft.com/download):

```bash
cd backend/src/Evalua.Api
dotnet restore
dotnet ef --version    # optional: ensures design-time tools resolve
dotnet run
```

Default URLs (see `Properties/launchSettings.json`): HTTPS `https://localhost:7189`, HTTP `http://localhost:5191`. Swagger UI: **`/swagger`**.

Smoke check without hitting client data:

- `GET /health` → `{ "status": "ok", "service": "Evalua.Api" }`

Paginated clients + country:

- `GET /api/clientes/sp?page=1&pageSize=10` → via `dbo.usp_ClientesPaginados`
- `GET /api/clientes/ef?page=1&pageSize=10` → via EF Core LINQ (`Include`, `Skip`, `Take`, `Select`)

Connection string placeholder (Windows auth example):

```
Server=localhost;Database=EvaluaClientes;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true
```

For macOS/Linux use `User ID=` / `Password=` or Docker SQL Server accordingly.

---

## Solution layout

| Path | Role |
|------|------|
| `Evalua.Platform.sln` | Solution |
| `src/Evalua.Api/` | ASP.NET Core host (`Program.cs`), controllers, EF context |
| `src/Evalua.Api/Entities/` | `Cliente`, `Pais` POCOs mapped to existing tables |
| `src/Evalua.Api/Data/AppDbContext.cs` | Fluent mapping to `dbo.Clientes` / `dbo.Paises` |

## Architecture snapshot

```
Client (Angular later)
       │ HTTP JSON
       ▼
Evalua.Api  ←── AppDbContext → SQL Server (tables + SP)
```

## Tech stack references

| Technology | Purpose |
|------------|---------|
| **ASP.NET Core** | HTTP pipeline, controllers, dependency injection container |
| **EF Core** | ORM mapping; LINQ queries in a later endpoint |
| **Swashbuckle** | Swagger / OpenAPI in Development |
| **SQL Server** | Relational persistence; scripts under `db/` |
