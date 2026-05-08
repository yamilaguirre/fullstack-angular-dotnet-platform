# Backend (.NET API)

ASP.NET Core **Web API** targeting **.NET 8**, **Entity Framework Core** (SQL Server), and two endpoints that return paginated clients with origin country:

1. Implemented via **stored procedure**.
2. Implemented via **LINQ / EF Core** query methods.

## Database (SQL Server)

Scripts live in [`db/`](db/) and run in order:

| File | Purpose |
|------|---------|
| `001-schema.sql` | Tables `Paises`, `Clientes`, FK |
| `002-seed.sql` | Sample countries & clients |
| `003-stored-procedure.sql` | `dbo.usp_ClientesPaginados` |

Create a database (e.g. `EvaluaClientes`), then execute the scripts against it in SSMS / Azure Data Studio / `sqlcmd`.

### Stored procedure smoke test

```sql
EXEC dbo.usp_ClientesPaginados @PageNumber = 1, @PageSize = 3;
EXEC dbo.usp_ClientesPaginados @PageNumber = 2, @PageSize = 3;
```

Expect `NombrePais` populated, `TotalRegistros = 8`, and different `PageNumber` / paging window.

---

## Source code (.NET API)

Solution and projects are generated in Phase 2+ (`src/`).

## Architecture (preview)

```
backend/
├── src/<ApiProject>/
└── db/
```
