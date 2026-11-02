# Development environment

Recorded from command output and screenshots supplied by the project owner (first record 2026-10-31, last updated 2026-11-02). Items marked *(reported)* were stated by the owner, not captured from a command.

## Verified versions

| Item | Value | Source |
|---|---|---|
| OS | Windows 11, build 10.0.26200, x64 | `dotnet --info` |
| Visual Studio | 2026, version **18.11.3** | `vswhere` |
| .NET SDK (active) | **10.0.403** (10.0.402 also installed). Pinned in `global.json`. | `dotnet --list-sdks` |
| MSBuild | 18.9.15+5a2162bea | `dotnet --info` |
| .NET host / runtimes | Host 10.0.14. Runtimes 10.0.13 and 10.0.14, plus older 6.0 and 8.0. | `dotnet --info` |
| Workload mode | Loose manifests (`10.0.400-manifests.899c35d8`), no workload sets | `dotnet --info` |
| Git | 2.56.0.windows.4 | `git --version` |
| MySQL Server | **8.4.13** Community (GPL), local install | MySQL Workbench connection banner |
| MySQL client | MySQL Workbench | owner's screenshot |

### Workloads

| Workload | Manifest version | Used by this project |
|---|---|---|
| android | 36.1.118/10.0.100 | Yes |
| maui | 10.0.110/10.0.100 | Yes |
| maui-windows | 10.0.110/10.0.100 | Yes |
| ios | 26.5.10315/10.0.100 | No (installed by Visual Studio, out of scope) |
| maccatalyst | 26.5.10315/10.0.100 | No (installed by Visual Studio, out of scope) |

### NuGet packages and tools (pinned by the first restore, 2026-11-02)

| Package or tool | Version | Where |
|---|---|---|
| Microting.EntityFrameworkCore.MySql | 10.0.14 | `LakbayMarket.Infrastructure` |
| Microsoft.EntityFrameworkCore.Design | 10.0.14 | `LakbayMarket.Api` (design-time only) |
| dotnet-ef | 10.0.14 | local tool, `dotnet-tools.json` |

Transitive versions (for example `Microsoft.EntityFrameworkCore.Relational`) are not recorded yet. Capture them with `dotnet list package --include-transitive`. See [ADR 0001](decisions/0001-database-and-ef-core-provider.md) for why this provider.

## Reported by the owner

| Item | Value |
|---|---|
| Windows Developer Mode | On *(reported)* |
| MAUI template app | Ran on Windows and on Android *(reported 2026-11-02; the device or emulator used is not recorded)* |
| Android emulator | Can lag on this machine (reported 2026-11-01). A physical phone is the fallback. |

## Local services

| Item | Value |
|---|---|
| Database | `lakbay_market_dev` on `localhost:3306` |
| Application account | `lakbay_app`, privileges limited to `lakbay_market_dev` |
| API (HTTPS / HTTP) | `https://localhost:7180` / `http://localhost:5180` |
| Connection string | In `dotnet user-secrets` for `LakbayMarket.Api`. Never in the repo. |

## Open items

| Item | State |
|---|---|
| Android SDK platforms installed | Not recorded (API 36 expected) |
| Git host and CI | GitHub (private repo). GitHub Actions builds the API and runs the integration tests on Linux (green, reported 2026-11-02). Not in CI yet: the MAUI app and a MySQL-backed migration check. |
| Java | `java` on PATH is 1.8.0_401. The Android build works, so no action unless an Android build reports a JDK error. |

## First-time local setup (new machine or fresh clone)

1. Install the tools in the version table: Visual Studio 2026 with the MAUI and ASP.NET workloads, the pinned .NET SDK, Git, MySQL 8.4 LTS and MySQL Workbench.
2. In MySQL, as an administrator, create the database and the application account. Pick your own password (letters, digits, `_` and `-` keep the connection string simple):

   ```sql
   CREATE DATABASE lakbay_market_dev CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;
   CREATE USER 'lakbay_app'@'localhost' IDENTIFIED BY 'YOUR-PASSWORD';
   GRANT ALL PRIVILEGES ON lakbay_market_dev.* TO 'lakbay_app'@'localhost';
   ```

3. Trust the development HTTPS certificate once: `dotnet dev-certs https --trust`
4. Store the connection string as a secret. Leave the quoted label as it is; type the password when prompted (it shows as asterisks):

   ```powershell
   $secure = Read-Host "Password for lakbay_app" -AsSecureString
   $pw = [System.Net.NetworkCredential]::new("", $secure).Password
   "Password length: $($pw.Length)"     # must not be 0
   dotnet user-secrets set "ConnectionStrings:Default" "Server=localhost;Port=3306;Database=lakbay_market_dev;User=lakbay_app;Password=$pw" --project src/LakbayMarket.Api
   ```

5. Restore the repo's tools and apply the migrations:

   ```powershell
   dotnet tool restore
   dotnet ef database update --project src/LakbayMarket.Infrastructure --startup-project src/LakbayMarket.Api
   ```

6. Run the API and open `https://localhost:7180/api/v1/health/ready`. Expected: `{"status":"Ready", ...}`

   ```powershell
   dotnet run --project src/LakbayMarket.Api --launch-profile https
   ```

7. Run the tests. They use a fake, unreachable database and never touch yours:

   ```powershell
   dotnet test src/LakbayMarket.Tests.Integration
   ```

## Troubleshooting

- **`NU1100 ... PackageSourceMapping is enabled` or "no source found under the specified package ID".** A package source mapping in `%APPDATA%\NuGet\NuGet.Config` lists exact package names, and Visual Studio adds names to it as packages are installed. On the owner's machine the fix was to back the file up and add `<package pattern="*" />` inside its `nuget.org` block. This is a per-machine setting, not part of the repo.
- **`dotnet ef --version` prints 9.x.** An older global copy is being used. Run `dotnet tool restore` in the repo root. The repo-local 10.0.x tool takes precedence there.
- **`Access denied ... (using password: NO)`.** The stored password is empty, usually because the `Read-Host` step was skipped or the password was typed into the quoted label. Redo step 4 and check that the length is not 0.
- **Never paste a terminal transcript that contains a real password.** Replace it with `****` first.

## Observations

- The installed MAUI manifest is **10.0.110**. Microsoft's support page listed 10.0.120 as the latest patch (2026-10-21) when checked on 2026-10-31. Stay on the installed version until the foundation is stable, then update deliberately and re-record.
- MAUI 10 support ends **2027-05-11**. Plan the move to the next MAUI release in the first half of 2027.
- `global.json` pins SDK `10.0.403` with `rollForward: latestPatch`.

## Change log

- 2026-10-31: initial record.
- 2026-11-01: MAUI spike passed (reported). Decisions: local MySQL 8.4 LTS, GitHub.
- 2026-11-02: MySQL 8.4.13 installed. Provider and tool versions pinned. `InitialCreate` migration applied. Setup and troubleshooting steps recorded.
- 2026-11-02: API integration tests and a GitHub Actions workflow added; first run green (reported).
