# Fullstack Angular + .NET Platform

Monorepo for the Evalúa Consultores practical exercise: **Angular** consumes a **.NET 8 REST API** on **SQL Server**. Two paginated backends expose the same payload:

- `GET /api/clientes/sp` → `dbo.usp_ClientesPaginados`
- `GET /api/clientes/ef` → **Entity Framework Core + LINQ**

## Repository layout

| Path | Responsibility |
|------|----------------|
| [`backend/`](backend/) | ASP.NET Core Web API, EF Core, SQL scripts (`db/`). |
| [`frontend/`](frontend/) | Angular standalone app, `HttpClient`, phone pipe, Jasmine/Karma. |

Detailed instructions live in each folder README.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js LTS](https://nodejs.org/) (recommended for Angular tooling)
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) or compatible host

## End-to-end workflow

### 1. Database

Create a database (default name `EvaluaClientes`) and execute, in order:

1. `backend/db/001-schema.sql`
2. `backend/db/002-seed.sql`
3. `backend/db/003-stored-procedure.sql`

### 2. Backend API

```bash
cd backend/src/Evalua.Api
# Ensure appsettings.json DefaultConnection points at your instance
dotnet restore
dotnet run
```

- Swagger (Development): `https://localhost:7189/swagger`
- Health probe: `GET https://localhost:7189/health`

### 3. Frontend

```bash
cd frontend
npm install
npm run start
```

Browse to `http://localhost:4200`.

CORS is limited to `http://localhost:4200` while the API listens on the HTTPS/HTTP ports declared in `Properties/launchSettings.json` (`https://localhost:7189`, `http://localhost:5191` by default).

### 4. Automated tests (frontend)

```bash
cd frontend
npx ng test --no-watch --browsers=ChromeHeadless
```

## Implementation phases (historical)

1. **Phase 0:** Repository scaffold + documentation skeleton.
2. **Phase 1:** SQL schema, seed, paginated stored procedure.
3. **Phase 2:** ASP.NET Core host + EF Core mappings + health endpoint.
4. **Phase 3:** `/api/clientes/sp` implementation.
5. **Phase 4:** `/api/clientes/ef` LINQ implementation.
6. **Phase 5:** CORS for local Angular.
7. **Phase 6–7:** Angular workspace, list UI, phone pipe, Karma specs.

## License

Private / assessment use unless otherwise noted.
