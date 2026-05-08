# Frontend (Angular)

Standalone **Angular 19** app (`evalua-client`) served with `ng serve`.

## What it does

- Calls the .NET API (**EF** or **stored procedure** endpoints) and shows a **paginated table** of customers with **country** (`nombrePais`).
- Formats telephone numbers in the grid with the `telefonoFormat` **pipe** (`+56912345678` → `+569 1234 5678`).
- Unit tests with **Jasmine + Karma** (`telefono-format.pipe.spec.ts`, `app.component.spec.ts`).

## Prerequisites

- Node.js **LTS** recommended (even-numbered major). The workspace was generated with `npm`.
- Chrome (or Chromium) for Karma when not using headless.

## Install & run

```bash
cd frontend
npm ci   # or npm install
npm run start   # http://localhost:4200
```

Configuration file `src/environments/environment.ts` swaps to `environment.development.ts` during `ng serve` / development build via `angular.json` `fileReplacements`. By default the API base URL is `http://localhost:5191`.

## Tests

```bash
cd frontend
npx ng test --no-watch --browsers=ChromeHeadless
```

## Project map

| Area | Path | Notes |
|------|------|--------|
| Routing | `src/app/app.routes.ts` | Lazy-loads `ClientesListComponent` at `/` |
| Shell | `src/app/app.component.*` | Hosts `<router-outlet />` |
| Feature UI | `src/app/features/clientes-list/` | Signals + RxJS `switchMap` reload stream |
| HTTP | `src/app/core/services/clientes.service.ts` | Builds `/api/clientes/{ef|sp}` URLs |
| Models | `src/app/core/models/` | Mirrors API DTOs (`camelCase` JSON) |
| Pipe | `src/app/shared/pipes/telefono-format.pipe.ts` | Presentation-only transform |

## Stack vocabulary

- **Standalone APIs:** components declare `imports` instead of NgModules.
- **`provideHttpClient()`:** registers `HttpClient` for dependency injection.
- **`@for` / `@if`:** modern built-in control-flow syntax (Angular 17+).
- **Signals:** `signal()` + `()` reads keep component state explicit.
- **RxJS `switchMap`:** cancels the previous HTTP request whenever the user triggers another reload (pagination / filters).
