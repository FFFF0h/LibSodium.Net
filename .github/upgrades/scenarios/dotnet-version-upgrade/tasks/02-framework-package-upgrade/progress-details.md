# 02-framework-package-upgrade Progress

## Changes Completed

- Updated `global.json` from SDK `8.0.402` to `10.0.400`, retaining `latestMajor` roll-forward behavior.
- Retargeted all ten repository projects from .NET 8-derived TFMs to .NET 10-derived TFMs:
  - Five solution projects now target `net10.0`.
  - Windows, Linux, and macOS test hosts now target `net10.0`.
  - Android and iOS test hosts now target `net10.0-android` and `net10.0-ios`.
- Removed the redundant `System.Memory` 4.6.0 package reference from `LibSodium.Net.csproj`; all other assessed package versions remain compatible.
- Updated GitHub Actions SDK setup and published-output paths from .NET 8 to .NET 10.
- Updated local build scripts and NuGet package documentation for .NET 10.
- Added `FindEntryPoint.Program` as the explicit startup object to resolve the generated TUnit entry-point conflict.
- Resolved .NET 10 compiler/analyzer findings in stream I/O, cancellation propagation, span default detection, assertions, and tests.
- Added `SpanExtensions.IsDefault` to preserve the existing semantic distinction between omitted/default spans and explicit empty spans without CA2265 violations.
- Refactored `SecureMemory<T>` to the standard `Dispose(bool)` pattern, allowing its finalizer cleanup path to be tested without reflection and remain compatible with Native AOT.

## Validation

- `dotnet restore src/LibSodium.Net.sln`: passed.
- `dotnet build src/LibSodium.Net.sln -c Release --no-incremental`: passed with zero warnings and errors.
- `dotnet publish src/LibSodium.Net.Tests/LibSodium.Net.Tests.Win.csproj -c Release`: passed for `net10.0/win-x64` Native AOT.
- Published Windows Native AOT suite: passed, 671/671 tests.
- Focused managed tests passed for `CryptoGenericHashTests` (38), `SecureMemoryTests` (58), `SecretStreamTests` (92), `SecretBoxTests` (38), and `CryptoBoxTests` (46).
- Full managed test project on the current ARM64 host: 658 passed and 15 failed. These are the same baseline failures observed before retargeting: 13 AES-256-GCM tests where the host/native implementation does not support the operation, plus 2 Ristretto scalar-reduction tests. The successful Windows x64 Native AOT run confirms no migration regression on the supported CI target.
- `dotnet list src/LibSodium.Net.sln package --vulnerable --include-transitive`: no vulnerable packages found.
- Repository scan found no stale active .NET 8 target, SDK, or automation references. The remaining `.NET 8+` text in `docfx/guide/MAC.md` describes the historical availability of UTF-8 literals and is intentionally unchanged.

## Issues Resolved

- Fixed CA1835, CA2016, CA2000, CA2264, CA2265, IDE0005, IDE0018, IDE0161, CS0121, and CS8892 findings exposed by the .NET 10 SDK.
- Fixed NETSDK1047 during out-of-solution Windows host publishing by performing a project-specific restore as part of `dotnet publish`.
- Fixed a Native AOT-only test failure caused by reflective finalizer invocation, replacing it with direct coverage of the shared finalizer cleanup path.

## Plan Deviation

The assessment and original plan listed only the five projects in `LibSodium.Net.sln`. Repository inspection found five additional platform test hosts and automation paths outside the solution. The task scope was expanded so the All-at-Once migration covers all ten projects and does not leave CI or platform builds pinned to .NET 8.
