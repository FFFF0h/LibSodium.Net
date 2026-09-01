# Task 05 Progress Details

## Validation completed

- Restored `src/LibSodium.Net.sln` successfully.
- Built the complete solution in Release with zero warnings and zero errors.
- Published the Windows x64 Native AOT test host and passed all 671 tests.
- Validated the Linux and macOS platform hosts at build level; cross-OS Native AOT publishing remains unavailable from Windows.
- Built the Android host. The retained `libsodium` 1.0.20.1 native package emits XA0141 for Android 16 KB page-size compatibility; this is an upstream native-package limitation.
- Corrected the iOS host runtime identifier from `linux-x64` to `ios-arm64`. Restore succeeds and the host builds; the Windows-hosted build reports the expected linker-disabled warning because no Mac build host is connected.
- Rechecked all solution packages for known vulnerabilities and deprecations; none were reported from the configured sources.
- Ran `git diff --check`; no whitespace errors were found, and generated `bin`, `obj`, and `TestResults` outputs remain untracked.

## Issues resolved

- Tested `libsodium` 1.0.22 as the suggested Android package update. Runtime validation proved it incompatible with the binding's generated 1.0.20 version contract, causing 495 Windows Native AOT failures. Reverted to 1.0.20.1, cleaned stale outputs, and restored the 671/671 passing baseline.
- Corrected the invalid iOS RID to use the package's `ios-arm64` native assets.

## Accepted environment limitations

- Linux/macOS Native AOT publish requires a matching OS runner.
- A complete linked iOS application build requires a connected Mac build host.
- Android XA0141 cannot be removed without adopting a newer native libsodium contract, which is outside this .NET/C# modernization scope.
- The previously documented ARM64 managed-host baseline remains 15 native/platform failures (13 AES-256-GCM and 2 Ristretto scalar-reduction cases); Windows x64 Native AOT is the supported local/CI validation target and passes completely.
