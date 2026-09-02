# LibSodium.Net

**LibSodium.Net** provides idiomatic .NET 10 bindings for [libsodium 1.0.22](https://doc.libsodium.org/). The API uses spans and secure memory where appropriate, supports Native AOT through source-generated interop, and remains compatible with libsodium data formats.

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

```console
dotnet add package LibSodium.Net
```

Or from the Visual Studio Package Manager Console:

```powershell
Install-Package LibSodium.Net
```

## Quick Start

```csharp
using System.Text;
using LibSodium;

Span<byte> key = stackalloc byte[XChaCha20Poly1305.KeyLen];
RandomGenerator.Fill(key);

byte[] plaintext = Encoding.UTF8.GetBytes("Hello world");
byte[] ciphertext = new byte[plaintext.Length + XChaCha20Poly1305.MacLen + XChaCha20Poly1305.NonceLen];
XChaCha20Poly1305.Encrypt(ciphertext, plaintext, key);

byte[] decrypted = new byte[plaintext.Length];
XChaCha20Poly1305.Decrypt(decrypted, ciphertext, key);
```

## Testing

The TUnit test suite targets .NET 10 and covers the supported cryptographic APIs, secure-memory behavior, and platform interop.

## Contributing

Issues and pull requests are welcome.

## License

Apache-2.0. See [LICENSE](LICENSE).
