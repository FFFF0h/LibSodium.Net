# 05-solution-validation: Validate the complete modernized solution

Perform final restore, Release build, and full test-suite validation for all projects after framework, package, and language changes are complete. Review the final diff for accidental generated artifacts, unresolved diagnostics, vulnerable packages, or deviations from the confirmed scope.

**Done when**: The full solution restores and builds in Release with zero warnings and errors, all tests pass, package checks reveal no unresolved vulnerabilities or conflicts, and modernization results are documented for review.

## Research Findings

- **Validation scope**: five projects in `src/LibSodium.Net.sln`, five platform-specific test hosts outside the solution, GitHub Actions/build scripts, package vulnerability state, Roslyn diagnostics, and final Git diff hygiene.
- **Build tools**: all projects are SDK-style. Use `dotnet restore`/`dotnet build` for the solution; use project-specific `dotnet publish` for desktop Native AOT hosts. Android/iOS require installed workloads and platform-aware validation.
- **Test baseline**: Windows x64 Native AOT is the supported local/CI target and currently passes 671/671. The ARM64 managed host consistently has 15 pre-existing native/platform failures (13 AES-256-GCM and 2 Ristretto scalar reduction); final validation must distinguish this known environment result from migration regressions.
- **Package baseline**: prior vulnerability scan found no vulnerable packages from configured sources.
- **Repository hygiene**: generated `bin`, `obj`, and `TestResults` outputs must remain untracked; final diff should include only intended source, project, automation, documentation, and workflow artifacts.
- **Done-when interpretation**: all supported/buildable local targets must pass. Workload- or architecture-limited targets and the known ARM64 native failures must be explicitly documented with successful Windows x64 Native AOT coverage.
- **Discovered platform fixes**: Android build exposed XA0141 in `libsodium` 1.0.20.1; the supported package query recommends 1.0.22, so update the direct library package and revalidate. iOS restore exposed an invalid `linux-x64` RID; the package contains `ios-arm64` native assets and the project should use `ios-arm64`.
- **Package compatibility result**: `libsodium` 1.0.22 was tested but is incompatible with the binding's generated 1.0.20 version constants, causing 495 Windows Native AOT failures. Keep 1.0.20.1 and document Android XA0141 as an upstream 16 KB page-size limitation rather than accepting a breaking native-version mismatch.

## Scope Inventory

- **Concerns**: restore/build, analyzers, managed and Native AOT tests, packages, platform hosts, automation references, and diff review.
- **Decomposition**: final validation is one gate whose checks are independent but collectively determine completion; no implementation subtasks are needed.
