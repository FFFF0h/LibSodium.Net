# 04-csharp14-semantic-modernization Progress

## Changes Completed

- Replaced `LibraryInitializer.initLock` with a private static readonly `System.Threading.Lock`.
- Replaced `TextFileLogger.lockObject` with a private static readonly `System.Threading.Lock`.
- Confirmed all four affected lock statements use the dedicated C# 13 lock semantics.
- Updated `csharp-language-modernization-assessment.md` with applied and skipped semantic candidates and rationale.

## Candidates Intentionally Skipped

- Kept `CryptoStreamHeader._chuckSize` because explicit native layout and endian conversion require a named `[FieldOffset]` field.
- Kept `SecretStream.TryReturnBuffer` unchanged because null-conditional assignment does not apply to its guarded method call.
- Kept explicit `.AsSpan()` conversions where they expose writable secure memory or document writable native interop arguments.
- Skipped extension blocks, partial members, compound assignment operators, and public `params Span` changes because they offer no concrete benefit or would alter compatibility.
- Kept all opt-in C# features excluded as requested.

## Validation

- Release solution build passed with zero warnings and errors.
- `LibSodium.Net` and `LibSodium.Net.Tests` Roslyn diagnostics contain no warnings.
- IDE0330 verification reports no pending `System.Threading.Lock` changes.
- Windows x64 Native AOT publish passed.
- Windows x64 Native AOT test suite passed: 671/671.

## Compatibility

- No public API signatures changed.
- No native interop declarations or explicit layouts changed.
- Lock fields are private and were verified to have no uses outside `lock` statements and no Monitor API dependencies.
