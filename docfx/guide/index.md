# Introduction to LibSodium.Net

[**LibSodium.Net**](https://github.com/LibSodium-Net/LibSodium.Net) provides .NET 10 bindings for [libsodium 1.0.22](https://doc.libsodium.org/) with span-based APIs, secure-memory support, and Native AOT-compatible interop.

_All code examples are written in C# and use the current LibSodium.Net API._

## Why LibSodium.Net?

- **Cross-platform testing**: The suite runs against native-AOT builds on Windows, Linux, and macOS.
- **Modern Cryptography**: Includes authenticated encryption, public-key cryptography, post-quantum key encapsulation, hashing, MACs, key derivation, and key exchange.
- **Simple and Secure API**: Designed to reduce complexity, helping you implement cryptography correctly and securely.
- **Secure Memory Handling**: Sensitive data management to minimize risks like memory leaks or data exposure.
- **Span<T> over Arrays**: Optimized for performance and memory efficiency by using `Span<T>` instead of heap-allocated arrays.
- **AOT Compatible**: Uses `LibraryImport` (source-generated P/Invoke) instead of `DllImport`, making it fully compatible with AOT compilation environments.

---

# Getting Started with LibSodium.Net

Here's how you can quickly integrate LibSodium.Net into your .NET projects.

## Install via NuGet

You can easily install LibSodium.Net using the NuGet package manager:

### CLI

```bash
dotnet add package LibSodium.Net
```

### Visual Studio

1. Right-click your project in Solution Explorer.
2. Choose **Manage NuGet Packages**.
3. Search for `LibSodium.Net` and click **Install**.

### Package Manager Console

```powershell
Install-Package LibSodium.Net
```

