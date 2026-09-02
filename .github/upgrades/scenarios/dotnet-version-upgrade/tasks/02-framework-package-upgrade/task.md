# 02-framework-package-upgrade: Upgrade every project to .NET 10

Atomically update all ten SDK-style projects to .NET 10-derived TFMs, preserving project references and build semantics. Address the assessment finding in `LibSodium.Net.csproj` where a NuGet package duplicates functionality included by the target framework, update CI/build scripts and output paths, then restore and resolve any .NET 10 compilation or package compatibility issues across the complete repository.

Research should begin with the solution project files, imported platform test hosts, the project dependency graph, automation scripts, and the `NuGet.0003` assessment issue. Test-project changes required by framework or package updates belong in this task.

**Done when**: Every project targets `net10.0`, redundant framework-provided package functionality is removed or otherwise resolved, restore succeeds, and the solution builds with zero warnings and errors.

## Research Findings

- All five solution projects define a singular, unconditional `TargetFramework` directly in their project file; each currently targets `net8.0` and should be replaced in place with `net10.0`.
- All projects are SDK-style and use standard per-project package management; no Central Package Management or repository-level MSBuild property file is involved.
- The installed and selected SDK is `10.0.400`. Root `global.json` currently requests `8.0.402` with `latestMajor`; pinning it to `10.0.400` makes the intended toolchain explicit while retaining roll-forward behavior.
- Assessment issue `NuGet.0003` applies only to the explicit `System.Memory` 4.6.0 reference in `LibSodium.Net.csproj`; .NET 10 provides this functionality and the package should be removed.
- `libsodium` 1.0.20.1, `Microsoft.Testing.Extensions.CodeCoverage` 17.14.2, and imported `TUnit` 0.19.143 are assessed as compatible and require no version change for this framework migration.
- No incompatible APIs, migration technologies, or `// STUB:` markers were detected.

## Scope Inventory

- **Projects affected**: `LibSodium.Net`, `LibSodium.Net.Tests`, `FindEntryPoint`, `LibSodium.Net.ReadPastAllocatedMemory`, and `LibSodium.Net.WriteReadOnlyProtectedMemory`.
- **Distinct concerns**: atomic TFM replacement, redundant package removal, SDK pin update, restore, and whole-solution compilation.
- **Dependency shape**: the library is shared by both memory-protection executables and the tests; `FindEntryPoint` references the test project. All project files therefore change together before restore/build.
- **Package actions**: remove `System.Memory`; preserve compatible explicit package versions.

## Expanded Repository Scope

- Five additional test hosts outside `LibSodium.Net.sln` import `LibSodium.Net.Tests.Core.csproj`: Windows, Linux, macOS, Android, and iOS. They must move to `net10.0`, `net10.0-android`, or `net10.0-ios` with the shared code.
- `.github/workflows/build-and-test.yml`, `src/build-test.cmd`, and `src/build-test-aot-macos.cmd` contain .NET 8 SDK, TFM, or output-path references that must follow the new targets.
- `src/LibSodium.Net/README.NuGet.md` advertises .NET 8 as the package minimum and should reflect the new .NET 10 minimum.
- The platform projects are not solution members, so the dependency analysis tool cannot evaluate them; direct inspection confirms they inherit shared packages/project references from `LibSodium.Net.Tests.Core.csproj`.
