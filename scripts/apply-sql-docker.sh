#!/usr/bin/env bash
# Apply SQL scripts to the Docker SQL Server started by docker-compose.yml.
# Prereq: container `evalua-sqlserver` running, SA password matches appsettings.Development.json / .env
set -euo pipefail

ROOT="$(cd "$(dirname "$0")/.." && pwd)"
CONTAINER="${SQL_CONTAINER:-evalua-sqlserver}"
PASS="${MSSQL_SA_PASSWORD:-Evalua_Dev_2026!}"

SQLCMD=(docker exec -i "$CONTAINER" /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$PASS" -C)

echo ">> Waiting for SQL Server (container: $CONTAINER)..."
for _ in {1..60}; do
  if docker exec "$CONTAINER" /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$PASS" -C -Q "SELECT 1" &>/dev/null; then
    break
  fi
  sleep 2
done

echo ">> 000-create-database.sql (master)"
"${SQLCMD[@]}" < "$ROOT/backend/db/000-create-database.sql"

echo ">> 001-schema.sql (EvaluaClientes)"
docker exec -i "$CONTAINER" /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$PASS" -C -d EvaluaClientes < "$ROOT/backend/db/001-schema.sql"

echo ">> 002-seed.sql (EvaluaClientes)"
docker exec -i "$CONTAINER" /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$PASS" -C -d EvaluaClientes < "$ROOT/backend/db/002-seed.sql"

echo ">> 003-stored-procedure.sql (EvaluaClientes)"
docker exec -i "$CONTAINER" /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$PASS" -C -d EvaluaClientes < "$ROOT/backend/db/003-stored-procedure.sql"

echo "Done. Verify with: EXEC dbo.usp_ClientesPaginados @PageNumber=1,@PageSize=3;"
