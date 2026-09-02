# LibSodium.Net

[![Build and Test](https://github.com/libSodium-net/LibSodium.Net/actions/workflows/build-and-test.yml/badge.svg)](https://github.com//libSodium-net/LibSodium.Net/actions/workflows/build-and-test.yml) [![NuGet](https://img.shields.io/nuget/v/LibSodium.Net.svg)](https://www.nuget.org/packages/LibSodium.Net/)

**LibSodium.Net** provides idiomatic .NET 10 bindings for [libsodium](https://doc.libsodium.org/) 1.0.22. The API uses spans and secure memory where appropriate, supports Native AOT through source-generated interop, and remains compatible with libsodium data formats.

The package supports Windows, Linux, macOS, iOS, Android, tvOS, and Mac Catalyst.

## Features

* Six AEAD algorithms with combined and detached modes, optional AAD, and automatic or manual nonces
* Secret-key and public-key authenticated encryption, digital signatures, and authenticated streams
* SHA-2, SHA-3, BLAKE2b, SipHash, HMAC, Poly1305, SHAKE, TurboSHAKE, and direct Keccak-f[1600] operations
* Argon2 and scrypt password hashing, HKDF, HChaCha20, and libsodium key derivation
* X25519 key exchange, Ristretto255 operations, ML-KEM768, and X-Wing key encapsulation
* IPcrypt address encryption, random generation, encoding, padding, and guarded secure memory
* High-level APIs plus low-level bindings for advanced use cases

## Design Philosophy

The library exposes libsodium primitives through a minimal C# API without introducing opaque key abstractions. It favors `Span<byte>`, `ReadOnlySpan<byte>`, strict input validation, and explicit control over key storage and protocol design.

## Documentation

The guide and generated API reference are available at [libsodium.net](https://libsodium.net/).

## Installation

LibSodium.Net targets `net10.0`. Install the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) and target .NET 10 in the consuming project.

Using the .NET CLI:

```console
dotnet add package LibSodium.Net
```

Using Visual Studio Package Manager Console:

```powershell
Install-Package LibSodium.Net
```

## Quick Start

```csharp
using System.Security.Cryptography;
using System.Text;
using LibSodium;

Span<byte> key = stackalloc byte[XChaCha20Poly1305.KeyLen];
RandomGenerator.Fill(key);

try
{
    byte[] plaintext = Encoding.UTF8.GetBytes("Hello LibSodium.Net!");
    byte[] aad = Encoding.UTF8.GetBytes("example-context");
    byte[] ciphertext = new byte[
        plaintext.Length + XChaCha20Poly1305.MacLen + XChaCha20Poly1305.NonceLen];

    XChaCha20Poly1305.Encrypt(ciphertext, plaintext, key, aad: aad);

    byte[] decrypted = new byte[plaintext.Length];
    XChaCha20Poly1305.Decrypt(decrypted, ciphertext, key, aad: aad);

    Console.WriteLine(Encoding.UTF8.GetString(decrypted));
}
finally
{
    CryptographicOperations.ZeroMemory(key);
}
```

## Native AOT

LibSodium.Net sets `IsAotCompatible` and uses source-generated `LibraryImport` interop. Consuming applications can enable Native AOT in their project file:

```xml
<PropertyGroup>
  <TargetFramework>net10.0</TargetFramework>
  <PublishAot>true</PublishAot>
</PropertyGroup>
```

Publish for a specific [runtime identifier](https://learn.microsoft.com/dotnet/core/rid-catalog):

```console
dotnet publish -c Release -r <RID>
```

For example, use `win-x64`, `linux-x64`, or `osx-arm64` as <RID>. 
Native AOT output is platform-specific and requires the [.NET Native AOT prerequisites](https://learn.microsoft.com/dotnet/core/deploying/native-aot/#prerequisites) for the target platform.

## Testing

The TUnit test suite targets .NET 10 and covers the supported cryptographic APIs, secure-memory behavior, and platform interop.

## Contributing

Issues and pull requests are welcome.

## License

Apache-2.0. See [LICENSE](LICENSE).
