# .NET Version Upgrade

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: `net10.0`
- **Target C# Version**: C# 14
- **C# Modernization Scope**: Default — apply safe and recommended changes; exclude opt-in changes unless explicitly requested
- **Namespace Style**: Use file-scoped namespaces in all applicable C# source files.
- **Using Directives**: Remove unnecessary using directives across C# source files.

## Decisions
- Proceed with full .NET 10 and C# 14 modernization despite the high token-usage estimate.
- Use All-at-Once strategy for all ten modern-.NET projects, including five platform test hosts outside the solution.

## Upgrade Options
**Source**: .github/upgrades/scenarios/dotnet-version-upgrade/upgrade-options.md

### Strategy
- Upgrade Strategy: All-at-Once

## Strategy
**Selected**: All-at-Once
**Rationale**: Ten projects use .NET 8-derived TFMs, with no high-risk migrations or incompatible packages detected.

### Execution Constraints
- Update all projects atomically; do not introduce dependency-tier sequencing or temporary multi-targeting.
- Update target frameworks and package references before restoring dependencies.
- Build and resolve all compilation errors in one bounded pass after the atomic upgrade.
- Run focused tests only after the upgraded solution builds without warnings or errors.
- Complete final full-solution validation after C# modernization.

## Source Control
- **Source Branch**: `main`
- **Working Branch**: `upgrade-dotnet-10`
- **Commit Strategy**: Single Commit at End
- **Branch Sync**: Auto (Merge)

## Build Tool Decisions
- **All solution projects**: `dotnet build` (SDK-style projects targeting modern .NET with no Visual Studio-only build features detected)
- **Platform test hosts outside the solution**: project-specific `dotnet publish` or workload-aware `dotnet build` validation.
