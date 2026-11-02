# Lakbay Market 2.0

Social travel, cultural discovery and local e-commerce app for **Android and Windows**.

*Discover more. Experience more. Shop local.*

## Status

**Week 1 — project foundation (in progress).** This list is updated as milestones land. Do not assume a feature exists unless it is listed here.

Exists and checked on the owner's machine (2026-11-02):

- **MAUI app:** the unmodified template runs on Windows and Android. There is no Lakbay UI yet.
- **API:** `GET /api/v1/health` (liveness) and `GET /api/v1/health/ready` (readiness, checks MySQL). The readiness endpoint was verified returning `Ready`.
- **Database:** an empty EF Core context and the `InitialCreate` migration, applied to local MySQL 8.4.
- **Tests and CI:** three integration tests for the API (health, a 404 returned as a Problem Details error, and the readiness endpoint returning 503 when the database is unreachable). GitHub Actions builds the API and runs them on Linux; the run was green on 2026-11-02 *(reported)*.

Not done yet: the app theme and navigation shell, a CI job for the MAUI app, a MySQL-backed migration check in CI, and every product feature.

## Planned stack

- Client: .NET MAUI 10 (C#, XAML), Android and Windows, MVVM with CommunityToolkit.Mvvm
- API: ASP.NET Core Web API on .NET 10 LTS, modular monolith
- Database: MySQL 8.4 with EF Core 10 ([decision record](docs/decisions/0001-database-and-ef-core-provider.md))
- Media: object storage and CDN (provider to be decided)
- Tests: xUnit. CI: GitHub Actions (API job only so far)

## Getting started

Tool versions and first-time setup (database, secrets, migrations, running the API) are in [`docs/dev-environment.md`](docs/dev-environment.md).

## Repository layout

```text
src/
  LakbayMarket.App/             MAUI app (Android + Windows), still the template
  LakbayMarket.Api/             ASP.NET Core API host
  LakbayMarket.Contracts/       request and response types shared by the app and the API
  LakbayMarket.Infrastructure/  EF Core context, migrations, provider wiring
  LakbayMarket.Tests.Integration/ xUnit tests that run the API in memory
docs/                           environment record, decision records
.github/workflows/              CI: build and test the API
```

More projects (Domain, Application, Admin, unit tests) are added milestone by milestone. The target layout is in the project blueprint.

## Secrets

Never commit passwords, tokens, signing keys or connection strings. Use `dotnet user-secrets` or environment variables locally and a managed secret store in hosted environments.
