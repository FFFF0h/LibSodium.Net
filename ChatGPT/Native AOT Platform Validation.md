# RFC: Native AOT Platform Validation

## Status

Implemented locally; native Linux, macOS, and iOS jobs require CI verification.

## Summary

This RFC defines repeatable Native AOT validation for LibSodium.Net and its Windows, Linux, macOS, iOS, and Android test hosts. Validation runs on operating systems that own each native toolchain, avoids cross-OS compilation, and treats warnings as failures without suppressing linker or analyzer diagnostics.

## Requirements

The implementation MUST:

- Keep `LibSodium.Net` compatible with trimming and Native AOT through `IsAotCompatible`.
- Publish and execute Windows, Linux, and macOS Native AOT test hosts on matching operating systems.
- Publish an unsigned Apple Silicon iOS simulator Native AOT bundle on a macOS runner with Xcode and `xcrun`.
- Compile the Android TUnit host with workload AOT enabled in Release builds.
- Use only stable packages and supported .NET workloads.
- Fail CI for compiler, analyzer, linker, restore, or test warnings and errors.
- Avoid pragma suppression or linker suppression descriptors for Native AOT validation.
- Allow the Android TUnit host to suppress `IL2037` through project-level `NoWarn` for TUnit's invalid optional F# lookup.

The implementation SHOULD:

- Keep platform test hosts outside the main solution.
- Keep Apple device signing outside the unsigned simulator validation path.
- Avoid publishing test-only dependencies transitively.
- Preserve platform-independent library behavior and native asset selection.

## Design

### Desktop Native AOT

Each desktop project owns one runtime identifier and runs on a matching GitHub Actions operating system:

| Platform | Project | Runtime identifier | Runner responsibility |
| --- | --- | --- | --- |
| Windows | `LibSodium.Net.Tests.Win.csproj` | `win-x64` | Publish and execute on Windows |
| Linux | `LibSodium.Net.Tests.Linux.csproj` | `linux-x64` | Publish and execute on Linux |
| macOS | `LibSodium.Net.Tests.Mac.csproj` | `osx-arm64` | Publish and execute on macOS |

CI MUST call `dotnet publish -c Release` for each project and execute its produced test binary. Cross-OS Native AOT publishing is not a supported fallback.

### iOS Native AOT

CI uses a macOS runner with the .NET 10 iOS workload and Xcode command-line tools. It verifies `xcrun` before publishing. The CI validation target is `iossimulator-arm64`, which avoids Apple signing while still exercising the Apple Native AOT compiler and linker.

The iOS project remains compatible with `ios-arm64` device publishing, but certificate and provisioning-profile validation is outside this RFC.

### Android Workload AOT

TUnit 1.65.68 contains an invalid dynamic dependency for its optional F# task adapter. Adding stable `FSharp.Core` resolves the assembly but still reports `IL2037` because TUnit names a member shape that current stable FSharp.Core does not expose. The Android host suppresses `IL2037` through its project-level `NoWarn` setting. Other linker and AOT diagnostic codes remain enforced. The test suite does not use F# tests, so the unreachable optional adapter does not affect its behavior.

Android excludes optional Microsoft Testing Platform code-coverage assets because the host does not collect coverage and the package's native Linux instrumentation library is not an Android runtime dependency.

## CI Structure

`build-and-test.yml` remains the authoritative workflow. It contains:

- Windows Native AOT publish and execution.
- Linux Native AOT publish and execution.
- macOS Native AOT publish and execution.
- iOS simulator Native AOT publish on macOS/Xcode.
- Android workload AOT publish with the Android workload installed.

Each job restores through its publish command so runtime-specific assets are present. Jobs MUST NOT reuse assets restored for a different runtime identifier.

## Validation

Required local validation on Windows:

1. Publish `LibSodium.Net.Tests.Android.csproj` in Release with workload AOT.
2. Publish and execute `LibSodium.Net.Tests.Win.csproj` in Release.
3. Run the managed `LibSodium.Net.Tests` suite.
4. Build `LibSodium.Net.sln` without warnings or errors.

Required CI validation:

1. Publish and execute Linux Native AOT host on Linux.
2. Publish and execute macOS Native AOT host on macOS.
3. Publish iOS simulator Native AOT bundle on macOS with `xcrun`.
4. Publish Android workload AOT host with stable dependencies.

## Compliance Matrix

| Requirement | Implementation | Validation | Status |
| --- | --- | --- | --- |
| Library trim/AOT analyzers | `IsAotCompatible=true` | Warning-free library build | Complete |
| Windows Native AOT | Existing Windows CI job | Local publish and 688 passing tests | Complete |
| Linux Native AOT | Existing Linux CI job | Linux CI publish and execution | Implemented; CI verification required |
| macOS Native AOT | Existing macOS CI job | macOS CI publish and execution | Implemented; CI verification required |
| iOS Native AOT | iOS AOT settings and `test-ios-aot` job | macOS/Xcode simulator publish | Implemented; CI verification required |
| Android workload AOT | TUnit Android host and Ubuntu CI job | Local Release AOT publish with warnings as errors | Complete |
| Stable dependencies only | Stable SDK, workload, action, and package references | No prerelease dependency added | Complete |
| TUnit diagnostic exception | Android project `NoWarn` for `IL2037` | TUnit AOT publish with other warning codes as errors | Complete |
| No additional AOT diagnostic suppression | Project and workflow review | Warning-free local AOT publishes | Complete |

## Rejected Alternatives

- Cross-OS Native AOT compilation: unsupported by the .NET Native AOT toolchain.
- Android `IL2035` suppression: hides an unresolved assembly and can produce invalid trimmed output.
- Adding `FSharp.Core` to the Android TUnit host: stable versions resolve the assembly but cannot repair TUnit's invalid `DynamicDependency` member name, producing `IL2037`.
- Link-attribute suppression for TUnit's exact method: unnecessary complexity compared with the Android project's explicit `NoWarn` exception.
- Signed iOS device publishing in general CI: requires secrets and provisioning that are unrelated to compile/link compatibility.
