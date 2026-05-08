# Backend — Evalúa.Api (.NET 8)

ASP.NET Core **Web API** with **Swagger** (Development), **Entity Framework Core** (SQL Server), and **two** paginated JSON endpoints returning **clients + country**:

| Endpoint | Implementation |
|----------|----------------|
| `GET /api/clientes/sp?page=&pageSize=` | Executes **`dbo.usp_ClientesPaginados`** (`FromSqlRaw` + keyless type). |
| `GET /api/clientes/ef?page=&pageSize=` | **LINQ**: `Include` → ordered query → `CountAsync`, `Skip`, `Take`, `Select` to DTO. |

JSON uses **camelCase** (`Program.cs`: `JsonNamingPolicy.CamelCase`).

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- A reachable **SQL Server** with database **`EvaluaClientes`** provisioned via [`db/README.md`](db/README.md) scripts.

---

## Connection strings

| File | Intended use |
|------|----------------|
| `appsettings.json` | Example **Windows Integrated Security**: `Trusted_Connection=True`. |
| `appsettings.Development.json` | **SQL authentication** tuned for Docker: `localhost,1433`, user `sa`, password aligned with Compose (default `Evalua_Dev_2026!`). |

**Do not ship real secrets.** For local overrides you can use [User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets):

```bash
cd src/Evalua.Api
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YOUR_CONNECTION_STRING"
```

---

## Run

```bash
cd backend/src/Evalua.Api
dotnet restore
dotnet run
```

URLs come from [`Properties/launchSettings.json`](src/Evalua.Api/Properties/launchSettings.json) (typically `https://localhost:7189`, `http://localhost:5191`). Swagger UI: `/swagger`.

### Smoke tests

```http
GET /health
GET /api/clientes/sp?page=1&pageSize=5
GET /api/clientes/ef?page=1&pageSize=5
```

Expected JSON shape:

```json
{
  "items": [
    {
      "idCliente": 1,
      "nombreCompleto": "…",
      "telefono": "…",
      "idPais": 1,
      "nombrePais": "…"
    }
  ],
  "page": 1,
  "pageSize": 5,
  "totalCount": 8
}
```

---

## Structure

| Area | Purpose |
|------|---------|
| `Controllers/` | `HealthController`, `ClientesSpController`, `ClientesEfController`. |
| `Data/AppDbContext.cs` | EF mappings for `Clientes` / `Paises`; keyless `ClientePaginadoSpRow`. |
| `Services/` | `ClientesStoredProcedureQuery`, `ClientesEntityFrameworkQuery`. |
| `Contracts/` | `ClienteDto`, `PagedResponse<T>`. |

---

## CORS

Policy **`AngularLocalhost`** allows `http://localhost:4200` for local Angular development (`Program.cs`).

---

## Tech stack

| Piece | Role |
|-------|------|
| ASP.NET Core | HTTP, DI, controllers |
| EF Core + `Microsoft.EntityFrameworkCore.SqlServer` | ORM + SQL provider |
| Swashbuckle | OpenAPI / Swagger UI |
