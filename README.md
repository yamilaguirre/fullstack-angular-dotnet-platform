# fullstack-angular-dotnet-platform

Ejercicio práctico (**Evalúa Consultores**) — tiempo límite orientativo ~5 h: aplicación **Angular** que consume una **API REST en .NET 8**, datos en **SQL Server**, listado **paginado** de clientes con **país de origen**, **pipe** de formato de teléfono y **pruebas unitarias** (Jasmine/Karma) del pipe.

This README is written so **any reviewer** can clone, install prerequisites, run database + API + SPA, and verify behaviour end-to-end.

---

## Contents (what you get)

| Layer | Highlights |
|--------|------------|
| **Database** | `Paises`, `Clientes`, `dbo.usp_ClientesPaginados` (OFFSET/FETCH + total count). Scripts under [`backend/db/`](backend/db/) — see [`backend/db/README.md`](backend/db/README.md). |
| **Backend** | ASP.NET Core Web API, EF Core, Swagger in Development, JSON **camelCase**. |
| **Endpoints** | `GET /api/clientes/sp` (stored procedure) and `GET /api/clientes/ef` (LINQ/EF), same paged JSON contract. |
| **Frontend** | Angular 19 standalone app: `HttpClient`, paginated table, `telefonoFormat` pipe, Karma specs. |
| **DevOps** | [`docker-compose.yml`](docker-compose.yml) for local SQL Server, [`scripts/apply-sql-docker.sh`](scripts/apply-sql-docker.sh) to load scripts, [`.github/workflows/ci.yml`](.github/workflows/ci.yml) (build + tests). |

More detail: [`backend/README.md`](backend/README.md), [`frontend/README.md`](frontend/README.md). Contributor notes: [`CONTRIBUTING.md`](CONTRIBUTING.md).

---

## Prerequisites

| Tool | Version / note |
|------|----------------|
| [.NET **8** SDK](https://dotnet.microsoft.com/download/dotnet/8.0) | `dotnet --version` → 8.x |
| [Node.js **LTS**](https://nodejs.org/) (even major) | e.g. 20.x or 22.x for Angular |
| [Docker Desktop](https://www.docker.com/products/docker-desktop/) | **Recommended** on macOS/Linux for SQL Server |
| Web browser | For Swagger + Angular UI |
| (Optional) [Azure Data Studio](https://aka.ms/azuredatastudio) / SSMS | Run SQL manually instead of the helper script |

---

## Quick start (Docker SQL + API + Angular)

### 1. Clone

```bash
git clone https://github.com/yamilaguirre/fullstack-angular-dotnet-platform.git
cd fullstack-angular-dotnet-platform
```

### 2. Start SQL Server

```bash
docker compose up -d
```

Optional: copy [`.env.example`](.env.example) to `.env` and change `MSSQL_SA_PASSWORD`. If you change it, **also** update `ConnectionStrings:DefaultConnection` in  
`backend/src/Evalua.Api/appsettings.Development.json` so the password matches.

Default dev password (documented, **not** for production): `Evalua_Dev_2026!`

Wait ~10–30 s for SQL to accept connections (first run may pull the image).

### 3. Load schema, data, and stored procedure

```bash
chmod +x scripts/apply-sql-docker.sh
./scripts/apply-sql-docker.sh
```

If `sqlcmd` is missing inside the container (rare with `mssql/server:2022-latest`), run the same files manually with your SQL client — order in [`backend/db/README.md`](backend/db/README.md).

### 4. Run the API

```bash
cd backend/src/Evalua.Api
dotnet restore
dotnet run
```

- **ASP.NETCORE_ENVIRONMENT** defaults to `Development` from `launchSettings.json` → uses **`appsettings.Development.json`** (SQL auth for Docker).
- Swagger: `https://localhost:7189/swagger` (or check console for listening URLs).
- Quick probe: `GET https://localhost:7189/health`

### 5. Run the Angular app

```bash
cd frontend
npm ci   # or npm install
npm run start
```

Open **http://localhost:4200**. The UI calls `http://localhost:5191` by default (see `frontend/src/environments/environment.development.ts`).

You should see a **Clientes** table, **pagination**, country column, and **telephone** values formatted like `+569 1234 5678`. Use the dropdown to switch between **EF** and **Stored procedure** data sources.

---

## Verify the assignment checklist (for evaluators)

| Requirement | How to verify |
|-------------|----------------|
| Angular **≥10** consuming a **service** | Open DevTools → Network: requests to `/api/clientes/ef` or `/sp`. Code: `ClientesService`, `ClientesListComponent`. |
| **Pipe** on phone field | Visually in the grid; implementation `frontend/src/app/shared/pipes/telefono-format.pipe.ts`. |
| **Jasmine/Karma** on the pipe | `npm run test:ci` in `frontend/` (or `npx ng test` interactively). |
| **.NET API** with **two** paginated services | Swagger: `GET /api/clientes/sp` and `GET /api/clientes/ef`. |
| First service uses **stored procedure** | `ClientesStoredProcedureQuery` + `usp_ClientesPaginados`. |
| Second uses **EF Core LINQ** | `ClientesEntityFrameworkQuery` (`Include`, `Skip`, `Take`, `Select`). |
| **SQL Server** | Scripts in `backend/db/`; Docker optional via `docker-compose.yml`. |

---

## Windows / SQL Server (integrated security) alternative

`appsettings.json` (non-Development) uses `Trusted_Connection=True` for a local Windows SQL instance. Point `Server` and `Database` to your instance, run the same SQL scripts on `EvaluaClientes`, then run the API with `ASPNETCORE_ENVIRONMENT=Production` **or** adjust configuration as needed — see [`backend/README.md`](backend/README.md).

---

## Automated CI

On push / PR to `main`, GitHub Actions runs:

- `dotnet build` on `backend/Evalua.Platform.sln` (Release)
- `npm ci` + `ng build` in `frontend/`
- `npm run test:ci` (Karma **ChromeHeadlessCI**)

---

## Project layout

```
├── backend/
│   ├── db/                  # SQL scripts (+ README order)
│   ├── Evalua.Platform.sln
│   └── src/Evalua.Api/       # ASP.NET Core project
├── frontend/                 # Angular workspace
├── scripts/apply-sql-docker.sh
├── docker-compose.yml
├── .env.example              # Optional override for MSSQL_SA_PASSWORD
└── .github/workflows/ci.yml
```

---

## License

Assessment / portfolio use unless otherwise noted.
