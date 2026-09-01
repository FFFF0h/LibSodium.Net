# 03-csharp14-mechanical-modernization: Apply safe C# 13 and C# 14 transformations

Assess the C# 12 to C# 14 language delta, scan the repository for compiler breaking-change patterns involving `field`, `extension`, `partial`, `scoped`, and changed Span overload resolution, and create the required C# modernization assessment artifact. Apply safe analyzer-backed transformations for C# 13 and C# 14 while preserving the repository's existing style configuration and public API compatibility.

Use targeted Roslyn diagnostics for applicable safe changes, including Lock modernization, static anonymous functions, unbound generic `nameof`, implicitly typed lambdas, and simple `field` accessors. Review generated-code and interop-heavy files carefully before formatting.

**Done when**: Breaking-change risks are documented and resolved, the C# modernization assessment records actual findings, safe mechanical transformations are applied, and the solution builds and tests without warnings or regressions.

## Research Findings

- **Version range**: C# 12 to C# 14, inferred from the original `net8.0` and target `net10.0` TFMs. Scope remains default: apply safe and recommended changes, excluding opt-in features.
- **Assessment consultation**: All five assessed solution projects are SDK-style and have no detected technologies or API compatibility findings relevant to language modernization. Their mandatory assessment findings were limited to TFM changes and the already-resolved redundant `System.Memory` reference.
- **Breaking keywords**: No source identifiers named `field` or `scoped` were found. `extension` appears only in a comment. The 235 `partial` occurrences are valid `LibraryImport` declarations and partial type declarations, not uses of `partial` as a return type.
- **Span overload resolution**: The .NET 10 build exposed an ambiguous mutable/read-only span assertion overload and `default` span comparisons; task 02 resolved these with a single `ReadOnlySpan<byte>` assertion overload and `SpanExtensions.IsDefault` while preserving behavior.
- **Analyzer candidates**: Analyzer-only `dotnet format` verification for IDE0330, IDE0320, IDE0340, IDE0350, and IDE0360 exits cleanly with no pending changes under the current inherited `.editorconfig`.
- **Manual candidates**: Four `lock` statements exist across `LibraryInitializer.cs` and `TextFileLogger.cs`; two generic `nameof` expressions are candidates for C# 14 unbound generic syntax. No ESC literals exist. No simple property-backing-field candidates were detected.
- **Span simplification candidates**: 113 `.AsSpan()` calls exist, but most are required conversions from `SecureMemory<T>` or writable-array access and require semantic review; they are deferred to task 04 rather than changed mechanically.
- **Formatting constraint**: The inherited `C:\source\FFFF0h\.editorconfig` uses spaces while the repository consistently uses tabs. Broad whitespace formatting would create unrelated churn, so only analyzer-specific transformations are permitted.
- **Stubs**: No `// STUB:` markers were found.

## Scope Inventory

- **Projects affected**: `LibSodium.Net` and `LibSodium.Net.Tests`; dependent executables and platform test hosts require build/test validation but currently have no direct language candidates.
- **Distinct concerns**: breaking-change audit, safe analyzer-backed syntax changes, modernization assessment artifact, build and test validation.
- **Decomposition**: The candidate set is small and independent, with one shared validation boundary; no subtask split is required.
