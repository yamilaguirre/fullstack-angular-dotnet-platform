# Fullstack Angular + .NET Platform

Monorepo for the Evalúa Consultores practical exercise: Angular client consuming a **.NET 8** REST API backed by **SQL Server**, with two paginated client listing strategies (stored procedure vs **EF Core / LINQ**).

## Repository layout

| Path | Responsibility |
|------|----------------|
| [`backend/`](backend/) | ASP.NET Core Web API, EF Core, SQL scripts (`db/`). |
| [`frontend/`](frontend/) | Angular application (HTTP client, phone format pipe, Jasmine/Karma). |

See each folder’s **README.md** for architecture and run instructions (populated as the project grows).

## Prerequisites (full stack — final state)

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js LTS](https://nodejs.org/) (for Angular CLI and `npm`)
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) (local, Docker, or Azure SQL with small adjustments)

---

## Getting started

1. **Phase 0:** repo structure, `.gitignore`, README stubs.
2. **Phase 1:** SQL Server scripts under `backend/db/`.
3. **Phase 2:** .NET 8 API scaffold with EF Core mapping and `GET /Health` (Swagger at `/swagger`).

Run the API: see [`backend/README.md`](backend/README.md). Angular UI comes in a later phase.

---

## License

Private / assessment use unless otherwise noted.
