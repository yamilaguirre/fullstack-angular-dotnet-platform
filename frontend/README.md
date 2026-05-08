# Frontend (Angular)

Angular application (**v10+**; project will use a current LTS line) that:

- Loads **Cliente** rows from the REST API (including **país**).
- Applies a **custom pipe** to format **teléfono** per the specification.
- Uses **Jasmine / Karma** for unit tests (including the phone pipe).

## Status

Phase 0: folder reserved. CLI-generated app arrives in a later phase.

## Structure (preview)

```
frontend/
├── src/app/                # Components, services, pipe
├── src/environments/       # API base URL per environment
└── angular.json, etc.
```
