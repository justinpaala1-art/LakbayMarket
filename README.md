# Lakbay Market 2.0

Social travel, cultural discovery and local e-commerce app for **Android and Windows**.

*Discover more. Experience more. Shop local.*

## Status

**Week 1 — project foundation (in progress).** Nothing is runnable yet. This file is updated as each milestone is built and tested; do not assume a feature exists until it is listed here.

## Planned stack

- Client: .NET MAUI 10 (C#, XAML), Android + Windows, MVVM with CommunityToolkit.Mvvm
- API: ASP.NET Core Web API on .NET 10 LTS, modular monolith
- Database: MySQL with EF Core
- Media: object storage + CDN (provider to be decided)
- Tests: xUnit; CI: GitHub Actions (to be confirmed)

## Prerequisites

- Visual Studio 2026 with the **.NET Multi-platform App UI development** and **ASP.NET and web development** workloads
- .NET SDK pinned in `global.json`
- Android SDK with an API 36 platform and an emulator image
- Windows Developer Mode enabled (for the Windows build)
- MySQL (local setup to be decided)

Exact versions recorded for this project are in [`docs/dev-environment.md`](docs/dev-environment.md).

## Repository layout

Only what exists today:

```text
docs/        environment record, later: architecture, API, decisions, runbooks
```

`src/` projects are added milestone by milestone. The planned target layout is in the project blueprint.

## Secrets

Never commit passwords, tokens, signing keys or connection strings. Use `dotnet user-secrets` or environment variables locally and a managed secret store in hosted environments.
