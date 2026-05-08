# Contributing

This repository targets a timed technical assignment. Prefer **focused** commits that map to one concern.

Suggested conventions:

- **Branches:** short-lived `feat/…`, `fix/…`, `chore/…` topics.
- **Commits:** [Conventional Commits](https://www.conventionalcommits.org/) (`feat(backend): …`, `fix(frontend): …`, `docs: …`).
- **Secrets:** never commit production credentials; use `.env` (gitignored) for Docker SQL password overrides and optionally [.NET User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) for API connection strings locally.
