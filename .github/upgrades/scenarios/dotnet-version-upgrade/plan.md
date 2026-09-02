# .NET 10 and C# 14 Modernization Plan

## Overview

**Target**: Upgrade the complete LibSodium.Net solution from .NET 8/C# 12 to .NET 10/C# 14 and apply safe plus recommended language modernizations.
**Scope**: Ten SDK-style projects: the LibSodium.Net library, FindEntryPoint utility, two memory-protection test applications, the primary tests, and five platform-specific test hosts, plus their CI/build scripts.

### Selected Strategy
**All-At-Once** — All projects upgraded simultaneously in a single operation.
**Rationale**: Ten projects, all on .NET 8-derived TFMs, with a clear dependency structure and no high-risk migrations detected.

## Tasks

### 01-toolchain-prerequisites: Validate the .NET 10 toolchain

Validate that the .NET 10 SDK is installed and that repository-level SDK selection, build configuration, and package restore infrastructure are compatible with the target framework. This task covers prerequisites only and must not alter project target frameworks.

**Done when**: The required SDK is available, any `global.json` constraint is compatible with .NET 10, and the existing solution baseline is understood before migration.

---

### 02-framework-package-upgrade: Upgrade every project to .NET 10

Atomically update all ten SDK-style projects to .NET 10-derived TFMs, preserving project references and build semantics. Address the assessment finding in `LibSodium.Net.csproj` where a NuGet package duplicates functionality included by the target framework, update CI/build scripts and output paths, then restore and resolve any .NET 10 compilation or package compatibility issues across the complete repository.

Research should begin with the solution project files, imported platform test hosts, the project dependency graph, automation scripts, and the `NuGet.0003` assessment issue. Test-project changes required by framework or package updates belong in this task.

**Done when**: Every project targets `net10.0`, redundant framework-provided package functionality is removed or otherwise resolved, restore succeeds, and the solution builds with zero warnings and errors.

---

### 03-csharp14-mechanical-modernization: Apply safe C# 13 and C# 14 transformations

Assess the C# 12 to C# 14 language delta, scan the repository for compiler breaking-change patterns involving `field`, `extension`, `partial`, `scoped`, and changed Span overload resolution, and create the required C# modernization assessment artifact. Apply safe analyzer-backed transformations for C# 13 and C# 14 while preserving the repository's existing style configuration and public API compatibility.

Use targeted Roslyn diagnostics for applicable safe changes, including Lock modernization, static anonymous functions, unbound generic `nameof`, implicitly typed lambdas, and simple `field` accessors. Review generated-code and interop-heavy files carefully before formatting.

**Done when**: Breaking-change risks are documented and resolved, the C# modernization assessment records actual findings, safe mechanical transformations are applied, and the solution builds and tests without warnings or regressions.

---

### 04-csharp14-semantic-modernization: Apply recommended C# 13 and C# 14 transformations

Review candidates that require semantic judgment, including private lock targets, null-conditional assignments, first-class Span simplifications, and appropriate property backing-field reductions. Apply only changes that preserve runtime behavior, binary/public API expectations, native interop correctness, and readability; skip extension blocks, partial members, compound operators, or params-Span API changes when their prerequisites or compatibility trade-offs are not satisfied.

Opt-in transformations such as nullable reference types, ref-struct interface adoption, and ignored directives remain excluded. Record applied and skipped candidates with reasons in the C# modernization assessment.

**Done when**: Applicable recommended transformations are complete, skipped candidates are documented, public and native interop behavior remains compatible, and focused builds/tests pass without warnings.

---

### 05-solution-validation: Validate the complete modernized solution

Perform final restore, Release build, and full test-suite validation for solution projects plus buildable platform hosts after framework, package, and language changes are complete. Review the final diff for accidental generated artifacts, unresolved diagnostics, vulnerable packages, stale automation paths, or deviations from the confirmed scope.

**Done when**: The full solution restores and builds in Release with zero warnings and errors, all tests pass, package checks reveal no unresolved vulnerabilities or conflicts, and modernization results are documented for review.
