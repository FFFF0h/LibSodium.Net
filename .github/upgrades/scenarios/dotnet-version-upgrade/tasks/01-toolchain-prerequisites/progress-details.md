# 01-toolchain-prerequisites Progress Details

## Completed Work

- Confirmed a compatible .NET 10 SDK is installed.
- Confirmed root `global.json` (`8.0.402`, `rollForward: latestMajor`) permits .NET 10 SDK selection.
- Confirmed all five projects are SDK-style modern-.NET projects and selected `dotnet build` for migration validation.
- Stabilized warnings-as-errors exposed by the .NET 10 SDK:
  - Replaced legacy array-based async stream reads with Memory-based overloads.
  - Forwarded `CancellationToken` to `ReadExactlyAsync`.
  - Removed the ambiguous mutable-Span `AssertLite.ShouldBe` overload; the ReadOnlySpan overload accepts both mutable and readonly spans.
  - Disposed the cancellation source in the async hash test.
  - Made finalizer coverage deterministic and analyzer-compliant by invoking the actual finalizer path while retaining owned disposal.
  - Applied targeted namespace, unused-using, and inline-out style fixes.

## Validation

- Solution build: passed with zero warnings and errors while projects still target .NET 8.
- `CryptoGenericHashTests`: 38/38 passed.
- `SecureMemoryTests`: 58/58 passed.
- `SecretStreamTests`: 92/92 passed.
- Full baseline suite: 658/673 passed. The 15 unrelated baseline failures are isolated to AES-256-GCM operations unavailable/failing in the current environment and two existing Ristretto scalar-reduction cases; none exercise the modified paths. These remain tracked for final validation and environment-aware handling.

## Files Modified

- `src/LibSodium.Net/StreamExtensions.cs`
- `src/LibSodium.Net/ICryptoIncrementalOperation.cs`
- `src/LibSodium.Net/SecretStream.cs`
- `src/LibSodium.Net.Tests/AssertLite.cs`
- `src/LibSodium.Net.Tests/CryptoGenericHashTests.cs`
- `src/LibSodium.Net.Tests/SecureMemoryTests.cs`
- `.github/upgrades/scenarios/dotnet-version-upgrade/scenario-instructions.md`
- `.github/upgrades/scenarios/dotnet-version-upgrade/tasks/01-toolchain-prerequisites/task.md`

## Issues Resolved

- CA1835, CA2016, CA2000, IDE0005, IDE0018, and IDE0161 baseline diagnostics.
- C# 14 Span-overload ambiguity in test assertions.
