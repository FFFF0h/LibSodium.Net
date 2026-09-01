# 03-csharp14-mechanical-modernization Progress

## Changes Completed

- Created `csharp-language-modernization-assessment.md` with the C# 12 to C# 14 feature inventory, breaking-change review, analyzer results, deferred recommendations, and excluded opt-in features.
- Audited `field`, `extension`, `partial`, `scoped`, and Span overload-resolution breaking changes. No unresolved keyword collisions remain; Span ambiguities found by the .NET 10 compiler were resolved in task 02.
- Ran analyzer-only modernization for IDE0330, IDE0320, IDE0340, IDE0350, and IDE0360. The final verification reports no pending fixes.
- Modernized two exception object-name expressions to C# 14 unbound generic `nameof` syntax:
  - `CryptoKeyLessHashIncremental<T>` to `nameof(CryptoKeyLessHashIncremental<>)`
  - `CryptoMacIncremental<T>` to `nameof(CryptoMacIncremental<>)`
- Preserved repository tab indentation and avoided broad formatting churn caused by the inherited space-based editor configuration.

## Validation

- `dotnet build src/LibSodium.Net.sln -c Release --no-incremental`: passed with zero warnings and errors.
- Analyzer verification for the five C# 13/14 rules: passed with no pending changes.
- Full managed ARM64 test run: 658 passed, 15 failed. Failures exactly match the established baseline: 13 AES-256-GCM cases unsupported by the current host/native path and 2 Ristretto scalar-reduction cases.
- Attempts to focus the TUnit 0.19.143 run with both `--filter` and `--treenode-filter` were ignored by the runner and executed all 673 tests; no new failures were observed.
- The Windows x64 Native AOT regression baseline from task 02 remains 671/671 passed.

## Deferred

- `System.Threading.Lock` changes and call-by-call Span simplifications require semantic review and belong to task 04.
- Extension blocks, partial members, compound assignment operators, public `params Span` changes, and all opt-in features are not justified for the mechanical pass.
