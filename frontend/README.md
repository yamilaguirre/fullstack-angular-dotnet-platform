# Frontend — evalua-client (Angular 19)

Standalone **Angular** application that fulfils the practical brief:

- Loads **clientes** from the REST API (including **país** / `nombrePais`).
- Server-side **pagination** (`page`, `pageSize` query params).
- Toggle in the UI between **`/api/clientes/ef`** and **`/api/clientes/sp`**.
- **`telefonoFormat` pipe** — displays numbers with a space every four characters from a normalized `+` prefix (e.g. `+56912345678` → `+569 1234 5678`).
- **Jasmine + Karma** specs, including **`telefono-format.pipe.spec.ts`**.

Angular version is **19** (CLI-generated workspace), satisfying the assignment requirement (**Angular 10 or higher**).

---

## Prerequisites

- **Node.js LTS** (recommended: **20.x** or **22.x**).
- **Chrome** locally for interactive `ng test` (CLI opens a browser unless you pass `--browsers=ChromeHeadless`).

---

## Install & serve

```bash
cd frontend
npm ci              # reproducible installs from package-lock.json
npm run start       # ng serve → http://localhost:4200
```

### API URL (environments)

- `environment.development.ts` → default API base **`http://localhost:5191`** (HTTP to avoid trusting dev HTTPS certificates).
- `angular.json` uses **`fileReplacements`** so **development builds** substitute `environment.ts` → `environment.development.ts`.

If your API listens elsewhere, edit `src/environments/environment.development.ts` (or swap ports in the backend `launchSettings.json`).

---

## Tests

Interactive:

```bash
npm test
```

CI / headless (same launcher GitHub Actions uses):

```bash
npm run test:ci
```

`karma.conf.js` defines **`ChromeHeadlessCI`** (`--no-sandbox`, `--disable-dev-shm-usage`) for Linux agents.

Production build smoke:

```bash
npm run build
```

---

## Code map

| Path | Responsibility |
|------|----------------|
| `src/app/app.routes.ts` | Lazy route → `ClientesListComponent`. |
| `src/app/features/clientes-list/` | Table, pagination UX, signals + RxJS reload (`switchMap`). |
| `src/app/core/services/clientes.service.ts` | `HttpClient.get` to `/api/clientes/{ef|sp}`. |
| `src/app/shared/pipes/telefono-format.pipe.ts` | Phone formatting + unit tests alongside. |

---

## Stack notes

- **Standalone components** — `bootstrapApplication` + `app.config.ts` providers (`provideHttpClient`, `provideRouter`).
- **Modern control flow** — `@if`, `@for` in templates.
- **Signals** — component state with `signal()` / `()`.
