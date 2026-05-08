# Handoff para un chat nuevo (Copilot / Agent en Cursor)

Este archivo es el **resumen vivo** del proyecto. En un chat nuevo, menciona que lea esto y las reglas del repo (`.cursor/rules/`).

## Mensaje sugerido para pegar al abrir un chat nuevo

> Sigue el proyecto `fullstack-angular-dotnet-platform` en esta carpeta. Lee **`docs/AI-HANDOFF.md`**, las reglas **`.cursor/rules/evalua-project.mdc`**, y el **`README.md`** raíz. No rehagas fases ya cerradas. El siguiente objetivo lo indico abajo.

Luego añade **una línea** con lo que quieres ahora (ej.: “ayúdame con Docker + SQL Server en Mac y cadena de conexión” o “Fase 9: GitHub Action que ejecute `ng test` y `dotnet build`”).

## Estado del repositorio (al crear este documento)

| Fase | Qué incluye |
|------|-------------|
| 0 | Monorepo, `.gitignore`, README raíz |
| 1 | `backend/db/` — esquema, seed, `usp_ClientesPaginados` |
| 2 | `backend/src/Evalua.Api/` — .NET 8, EF Core, entidades, Swagger, `GET /health` |
| 3 | `GET /api/clientes/sp` |
| 4 | `GET /api/clientes/ef` |
| 5 | CORS para `http://localhost:4200` |
| 6–7 | `frontend/` Angular 19 — listado paginado, pipe `telefonoFormat`, Karma |

**Evaluación entregable:** código en GitHub/GitLab/Bitbucket cumple el enunciado Angular + API + pruebas del pipe.

## Próximos pasos típicos en **tu máquina** (no son fases del código)

1. Instalar **.NET 8 SDK** (`dotnet --version` → 8.x).
2. Tener **SQL Server accesible** (en Mac suele ser **Docker** + imagen Microsoft); crear BD `EvaluaClientes` y ejecutar `001`, `002`, `003`.
3. Ajustar **`ConnectionStrings:DefaultConnection`** en `backend/src/Evalua.Api/appsettings.json` (o User Secrets) a tu instancia.
4. `dotnet run` en la API + `npm run start` en `frontend` y validar tabla + Network.

## Fase 9+ (opcional, por definir contigo)

Ideas comunes: `docker-compose` (SQL + app), workflow CI, tests xUnit en API, endurecer validación de paginación, Variable de entorno para URL API en Angular.

## Remoto Git

Nombre del repo público esperado: `fullstack-angular-dotnet-platform` (GitHub usuario `yamilaguirre`). Tras clones, ejecuta tus propios `git remote` locales.
