# SQL scripts (SQL Server)

Run against an existing **Microsoft SQL Server** instance. Default database name: **`EvaluaClientes`** (matches the API connection strings in this repo).

## Order of execution

| Order | File | Runs on DB | Purpose |
|:-----:|------|------------|---------|
| 0 | `000-create-database.sql` | **`master`** (or equivalent system DB) | Creates `EvaluaClientes` if missing |
| 1 | `001-schema.sql` | **`EvaluaClientes`** | Tables `Paises`, `Clientes`, FK |
| 2 | `002-seed.sql` | **`EvaluaClientes`** | Sample rows |
| 3 | `003-stored-procedure.sql` | **`EvaluaClientes`** | `dbo.usp_ClientesPaginados` |

After loading data, sanity check:

```sql
EXEC dbo.usp_ClientesPaginados @PageNumber = 1, @PageSize = 3;
```

Expect **eight** customers in total (`totalCount`/`TotalRegistros` semantics in API vs raw SP column name).

## Docker shortcut

From the repo root (with Compose SQL running):

```bash
chmod +x scripts/apply-sql-docker.sh
./scripts/apply-sql-docker.sh
```

Ensure `MSSQL_SA_PASSWORD` matches `backend/src/Evalua.Api/appsettings.Development.json` (Docker / macOS/Linux dev).
