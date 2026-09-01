# 04-csharp14-semantic-modernization: Apply recommended C# 13 and C# 14 transformations

Review candidates that require semantic judgment, including private lock targets, null-conditional assignments, first-class Span simplifications, and appropriate property backing-field reductions. Apply only changes that preserve runtime behavior, binary/public API expectations, native interop correctness, and readability; skip extension blocks, partial members, compound operators, or params-Span API changes when their prerequisites or compatibility trade-offs are not satisfied.

Opt-in transformations such as nullable reference types, ref-struct interface adoption, and ignored directives remain excluded. Record applied and skipped candidates with reasons in the C# modernization assessment.

**Done when**: Applicable recommended transformations are complete, skipped candidates are documented, public and native interop behavior remains compatible, and focused builds/tests pass without warnings.

## Research Findings

- `LibraryInitializer.initLock` is private, referenced only by one `lock` statement, and has no `Monitor` usage. It is safe to replace with `System.Threading.Lock`.
- `TextFileLogger.lockObject` is private, referenced only by three `lock` statements, and has no `Monitor` usage. It is safe to replace with `System.Threading.Lock`; making it `readonly` also preserves identity.
- `SecretStream.TryReturnBuffer` contains a null guard followed by a method call, not a member assignment, so C# 14 null-conditional assignment is inapplicable. Existing null handling remains clearer.
- The only explicit property backing field candidate is `CryptoStreamHeader._chuckSize`. It is an explicit-layout field with `[FieldOffset(12)]` and endian conversion in both accessors; synthesized `field` storage would break native layout and is prohibited.
- The 113 `.AsSpan()` calls are predominantly `SecureMemory<T>.AsSpan()` calls that expose writable unmanaged memory, or explicit writable array conversions passed to native interop. They are not redundant first-class Span conversions and should remain explicit.
- Extension blocks are not useful because existing extension types do not gain extension properties/operators; partial declarations are required by `LibraryImport`; compound operators and `params Span` changes would alter public/API semantics.
- No `// STUB:` markers or additional assessment issues apply.

## Scope Inventory

- **Projects affected**: `LibSodium.Net` and `LibSodium.Net.Tests`.
- **Changes**: two private lock-field type modernizations; documentation of intentionally skipped semantic candidates.
- **Validation**: warning-free solution build plus lock/initialization and Windows Native AOT tests.
- **Decomposition**: two small, independent field substitutions share the same build/test boundary; task remains atomic.
