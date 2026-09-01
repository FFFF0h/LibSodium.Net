# 01-toolchain-prerequisites: Validate the .NET 10 toolchain

Validate that the .NET 10 SDK is installed and that repository-level SDK selection, build configuration, and package restore infrastructure are compatible with the target framework. This task covers prerequisites only and must not alter project target frameworks.

**Done when**: The required SDK is available, any `global.json` constraint is compatible with .NET 10, and the existing solution baseline is understood before migration.

## Research Findings

- The .NET 10-compatible SDK is installed and available to the solution.
- Root `global.json` requests SDK `8.0.402` with `rollForward: latestMajor`; validation confirms this permits the installed .NET 10 SDK.
- All five projects use `Microsoft.NET.Sdk`, target `net8.0`, and can use `dotnet build`; no legacy MSBuild-only project features were found.
- No repository-level central package management, NuGet configuration, or custom build instruction file was found.
- Baseline build exposed three warnings-as-errors in the shared library: CA1835 in `StreamExtensions.cs` and `ICryptoIncrementalOperation.cs`, and CA2016 in `SecretStream.cs`. Downstream CS0006 diagnostics were cascades from the shared-library failure.
- Baseline stabilization uses Memory-based async stream overloads and forwards the existing cancellation token; these changes preserve pooled-buffer ownership and cancellation semantics.

## Scope Inventory

- **Projects affected**: `LibSodium.Net` directly; all four dependent projects require solution-level validation.
- **Distinct concerns**: SDK selection, build-tool selection, baseline analyzer compliance.
- **Build tool**: `dotnet build` for the SDK-style modern-.NET solution.
