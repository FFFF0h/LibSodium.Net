# C# Language Modernization Assessment

**Project:** LibSodium.Net
**Current version:** C# 12
**Target version:** C# 14
**Scope:** Default — safe and recommended changes; opt-in changes excluded
**Date:** 2026-09-01

## Summary

| Category | Findings | Action |
|---|---:|---|
| Breaking changes | 2 resolved, 0 outstanding | Preserve task 02 fixes and validate |
| Safe analyzer-backed | 0 outstanding | Analyzer verification clean |
| Safe manual | 2 files / 2 expressions | Use unbound generic `nameof` |
| Recommended | 2 files / 4 lock sites | Review private lock targets in task 04 |
| Opt-in | Not assessed for application | Excluded by scope |

## Phase 0: Breaking Changes

| Breaking change | Result | Mitigation |
|---|---|---|
| `field`, `extension`, `partial`, and `scoped` contextual interpretation | No conflicting identifiers found | No code change required |
| Expanded Span/ReadOnlySpan overload applicability | Two patterns surfaced during the .NET 10 build | Removed the ambiguous mutable-span assertion overload and replaced `default` span comparisons with explicit `IsDefault()` helpers |
| Partial interface members | No partial interfaces found | No code change required |
| Enumerator/disposal and redundant-pattern diagnostics | No outstanding build diagnostics | No code change required |

The repository's 235 `partial` matches are expected `LibraryImport` partial methods and their containing partial classes. They do not use `partial` as a return type.

## Phase 1: Analyzer-Backed Mechanical Changes

The targeted analyzer verification completed with no pending changes:

```text
IDE0330 Prefer System.Threading.Lock
IDE0320 Make anonymous function static
IDE0340 Use unbound generic type in nameof
IDE0350 Use implicitly typed lambda
IDE0360 Simplify property accessor with field
```

The inherited `C:\source\FFFF0h\.editorconfig` requests space indentation while this repository consistently uses tabs. Broad `dotnet format` whitespace changes are intentionally excluded to avoid reformatting unrelated code. Analyzer-only execution is used instead.

## Phase 2: Safe Manual Changes

| Feature | Candidate files | Decision |
|---|---|---|
| Unbound generic types in `nameof` | `CryptoKeyLessHashIncremental.cs`, `CryptoMacIncremental.cs` | Apply; exception object names are unchanged |
| `\e` escape sequence | None | Not applicable |
| Simple `field` accessors | None | Not applicable |
| Implicit lambda parameter types | None identified by analyzer | Not applicable |
| Static anonymous functions | None identified by analyzer | Not applicable |

## Recommended Changes for Task 04

| Feature | Candidate files | Review required |
|---|---|---|
| `System.Threading.Lock` | `LibraryInitializer.cs`, `TextFileLogger.cs` | Confirm each private object is used solely for locking and has no Monitor usage |
| First-class Span simplifications | 113 `.AsSpan()` calls across source/tests | Most operate on `SecureMemory<T>` or require mutable spans; validate individually and avoid public API or interop changes |
| Null-conditional assignment | `SecretStream.cs` cleanup guard | Current block also returns a pooled array, so direct assignment syntax does not apply |
| Extension blocks, partial members, compound operators | Interop-heavy library | Skip unless a concrete readability or generator use case exists |

## Opt-In Features Not Applied

- Nullable reference type adoption beyond existing project settings
- `ref struct` interface implementation
- Ignored directives / file-based apps
- Overload-resolution-priority attributes
- Public `params ReadOnlySpan<T>` API changes

## Validation Baseline

- Release solution build: zero warnings and errors.
- Windows x64 Native AOT suite: 671/671 passed.
- ARM64 managed suite: 15 known baseline native/platform failures (13 AES-256-GCM and 2 Ristretto scalar-reduction), unchanged by the framework migration.

## Execution Results

### Analyzer-backed phase: Complete

- Analyzer-only format pass applied for IDE0330, IDE0320, IDE0340, IDE0350, and IDE0360.
- Follow-up `--verify-no-changes` passed with no remaining applicable diagnostics.
- No broad whitespace formatting was applied.

### Safe manual phase: Complete

| Feature | Files changed | Result |
|---|---|---|
| Unbound generic `nameof` | `CryptoKeyLessHashIncremental.cs`, `CryptoMacIncremental.cs` | Replaced closed generic spellings with `nameof(Type<>)`; emitted names remain unchanged |

### Validation

- Release solution build passed with zero warnings and errors after the mechanical changes.
- Full ARM64 managed suite reproduced the unchanged baseline: 658 passed, 15 failed (13 unsupported AES-256-GCM cases and 2 Ristretto scalar-reduction cases).
- Targeted runner filters were not honored by TUnit 0.19.143 in this repository; no additional failures appeared beyond the known baseline.
- Task 02's Windows x64 Native AOT suite remains the supported regression baseline at 671/671 passed.

### Deferred to recommended modernization

- Four private lock sites for `System.Threading.Lock` review.
- First-class Span simplifications requiring call-by-call semantic analysis.
- No extension-block, partial-member, compound-operator, or `params Span` changes are mechanically justified.

## Recommended Semantic Modernization Results

### Applied

| Feature | Files | Result |
|---|---|---|
| `System.Threading.Lock` | `LibraryInitializer.cs`, `TextFileLogger.cs` | Replaced private `object` lock fields used exclusively by `lock`; the test logger field is now also `readonly` |

All four lock sites now use the dedicated C# 13 lock primitive. Symbol-usage analysis confirmed the fields are private lock targets only, and no `Monitor.Wait`, `Pulse`, or other Monitor API usage exists.

### Skipped

| Candidate | Reason |
|---|---|
| `CryptoStreamHeader._chuckSize` to C# 14 `field` | The field has `[FieldOffset(12)]` in an explicit-layout native header and both accessors perform endian conversion. Synthesized storage would break layout and behavior. |
| `SecretStream.TryReturnBuffer` null-conditional assignment | The guarded operation is an `ArrayPool.Return` method call, not assignment through a nullable receiver. |
| First-class Span simplification | Reviewed candidates predominantly call `SecureMemory<T>.AsSpan()` to expose writable unmanaged memory or explicitly create writable spans for native interop; removal would be invalid or reduce clarity. |
| Extension blocks | Existing extension helpers gain no property/operator benefit, while conversion would add early-adoption and consumer-language risk. |
| Partial properties/events/constructors | No source-generator-driven use case exists; existing partial declarations are required `LibraryImport` methods. |
| Compound assignment operators | No mutable numeric/container domain type benefits without changing semantics. |
| `params ReadOnlySpan<T>` | Would alter public API signatures and binary compatibility. |

### Semantic Validation

- Release solution build passed with zero warnings and errors.
- Roslyn project diagnostics report no warnings for `LibSodium.Net` or `LibSodium.Net.Tests`.
- IDE0330 analyzer verification passed with no remaining lock modernization candidates.
- Windows x64 Native AOT publish succeeded and all 671 tests passed after the lock changes.
