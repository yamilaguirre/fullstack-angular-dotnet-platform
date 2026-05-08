# Backend (.NET API)

ASP.NET Core **Web API** targeting **.NET 8**, **Entity Framework Core** (SQL Server), and two endpoints that return paginated clients with origin country:

1. Implemented via **stored procedure**.
2. Implemented via **LINQ / EF Core** query methods.

## Status

Phase 0: folder reserved. Source and `db/` scripts are added in later phases.

## Architecture (preview)

```
backend/
├── src/<ApiProject>/       # Entry: Program.cs / Controllers, DI
└── db/                     # DDL, seed data, CREATE PROCEDURE
```

Details will be updated when the solution is generated.
