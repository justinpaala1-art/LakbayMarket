# Development environment record

Recorded **2026-10-31** from command output supplied by the project owner. Items marked *(reported)* were stated by the owner, not captured from a command. Nothing here has been built yet.

## Verified from command output

| Item | Value | Source |
|---|---|---|
| OS | Windows 11, build 10.0.26200, x64 | `dotnet --info` |
| Visual Studio | 2026, version **18.11.3** | `vswhere ... catalog_productDisplayVersion` |
| .NET SDK (active) | **10.0.403** (10.0.402 also installed) | `dotnet --list-sdks` |
| MSBuild | 18.9.15+5a2162bea | `dotnet --info` |
| .NET host | 10.0.14 | `dotnet --info` |
| .NET runtimes | NETCore.App / AspNetCore.App / WindowsDesktop.App 10.0.13 and 10.0.14; also 6.0.36 and 8.0.x | `dotnet --info` |
| Workload mode | Loose manifests (workload version `10.0.400-manifests.899c35d8`); no workload sets installed | `dotnet --info` |
| Git | 2.56.0.windows.4 | `git --version` |

### Workloads

| Workload | Manifest version | Used by this project |
|---|---|---|
| android | 36.1.118/10.0.100 | Yes |
| maui | 10.0.110/10.0.100 | Yes |
| maui-windows | 10.0.110/10.0.100 | Yes |
| ios | 26.5.10315/10.0.100 | No (installed by Visual Studio, out of scope) |
| maccatalyst | 26.5.10315/10.0.100 | No (installed by Visual Studio, out of scope) |

## Reported by the owner

| Item | Value |
|---|---|
| Android emulator | A virtual device was created and boots *(reported)*. Device model and API level not yet recorded. |
| Windows Developer Mode | Turned on *(reported)* |

## Not ready or not yet recorded

| Item | State | Needed for |
|---|---|---|
| MySQL | `mysql` is not on PATH: not installed, or installed but not on PATH. Local vs Docker still to be decided. | Week 1 task 5 |
| Java | `java` on PATH is **1.8.0_401** (JDK 8). Not yet known whether the Android build uses it or a JDK configured in Visual Studio. Record `JAVA_HOME` and the JDK path in Visual Studio's Android settings; if the Android build reports a JDK error, start here. | Android build |
| Android SDK platforms installed | Not yet recorded (API 36 expected) | Android build |
| Docker | Not checked | Possible MySQL option |
| Git host and CI | Not yet decided (default: GitHub + GitHub Actions) | Week 1 task 6 |

## Observations

- The installed MAUI manifest is **10.0.110**. Microsoft's support policy page listed 10.0.120 as the latest patch (released 2026-10-21) when checked on 2026-10-31. Stay on the installed version until the foundation builds, then update deliberately and re-record.
- MAUI 10 support ends **2027-05-11**. Plan the move to the next MAUI release in the first half of 2027.
- `global.json` pins SDK `10.0.403` with `rollForward: latestPatch`.

## Change log

- 2026-10-31: initial record.
