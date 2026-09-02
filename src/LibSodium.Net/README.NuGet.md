# Modern cryptography for .NET 10

Idiomatic .NET bindings for [libsodium 1.0.22](https://doc.libsodium.org) with span-based APIs and secure-memory support.
Includes AEAD encryption (XChaCha20-Poly1305, AES256-GCM, AEGIS), public-key cryptography (`CryptoBox`, `Sealed Boxes`, `CryptoSign`), authenticated streaming (`SecretStream`), post-quantum key encapsulation, SHA-3/XOF hashing, IPcrypt, secure memory, and more.

Built for Windows, Linux, macOS, iOS, Android, tvOS, and Mac Catalyst.

Designed for efficient use with spans and Native AOT through `LibraryImport`.

Tested in GitHub Actions using AOT builds on Windows, Linux and macOS

## Documentation

See [libsodium.net](https://libsodium.net/) for guides and the API reference.

## libsodium 1.0.22

- `CryptoKem`: the recommended X-Wing hybrid KEM combining ML-KEM768 and X25519.
- `CryptoKemXWing`: X-Wing operations with deterministic encapsulation support for tests and published vectors.
- `CryptoKemMlKem768`: NIST-standardized post-quantum ML-KEM768.
- `CryptoSha3256` and `CryptoSha3512`: one-shot, stream, async, and incremental SHA-3 hashing.
- `CryptoShake128`, `CryptoShake256`, `CryptoTurboShake128`, and `CryptoTurboShake256`: one-shot and incremental extendable-output functions.
- `CryptoKeccak1600`: direct Keccak-f[1600] state operations and 12/24-round permutations.
- `CryptoIpCrypt`: deterministic, ND, NDX, and prefix-preserving IP address encryption.

```csharp
// X-Wing hybrid post-quantum key encapsulation
var publicKey = new byte[CryptoKem.PublicKeyLen];
var secretKey = new byte[CryptoKem.SecretKeyLen];
var ciphertext = new byte[CryptoKem.CiphertextLen];
var senderSecret = new byte[CryptoKem.SharedSecretLen];
var recipientSecret = new byte[CryptoKem.SharedSecretLen];

CryptoKem.GenerateKeyPair(publicKey, secretKey);
CryptoKem.Encapsulate(ciphertext, senderSecret, publicKey);
CryptoKem.Decapsulate(recipientSecret, ciphertext, secretKey);

Console.WriteLine($"Shared secret matches: {senderSecret.SequenceEqual(recipientSecret)}");
```

```csharp
// XChaCha20Poly1305 combined mode with an automatic nonce and AAD
Span<byte> key = stackalloc byte[XChaCha20Poly1305.KeyLen];
RandomGenerator.Fill(key);

var aad = Encoding.UTF8.GetBytes("context");
var data = Encoding.UTF8.GetBytes("Hello");

var ciphertext = new byte[data.Length + XChaCha20Poly1305.MacLen + XChaCha20Poly1305.NonceLen];
XChaCha20Poly1305.Encrypt(ciphertext, data, key, aad: aad);

var decrypted = new byte[data.Length];
XChaCha20Poly1305.Decrypt(decrypted, ciphertext, key, aad: aad);

var isWorking = decrypted.SequenceEqual(data);
Console.WriteLine($"It works: {isWorking}");
```

```csharp
// XChaCha20-Poly1305 authenticated encryption for streams
Span<byte> key = stackalloc byte[32];
RandomGenerator.Fill(key);

var helloData = Encoding.UTF8.GetBytes("Hello LibSodium.Net!");

using var plaintextStream = new MemoryStream();
using var ciphertextStream = new MemoryStream();
using var decryptedStream = new MemoryStream();

plaintextStream.Write(helloData);
plaintextStream.Position = 0;

SecretStream.Encrypt(plaintextStream, ciphertextStream, key);
ciphertextStream.Position = 0;
SecretStream.Decrypt(ciphertextStream, decryptedStream, key);
decryptedStream.Position = 0;

var isWorking = decryptedStream.ToArray().SequenceEqual(helloData);

Console.WriteLine($"It works: {isWorking}");
```

```csharp
// Authenticated public-key encryption with CryptoBox
Span<byte> senderPk = stackalloc byte[CryptoBox.PublicKeyLen];
Span<byte> senderSk = stackalloc byte[CryptoBox.PrivateKeyLen];
Span<byte> recipientPk = stackalloc byte[CryptoBox.PublicKeyLen];
Span<byte> recipientSk = stackalloc byte[CryptoBox.PrivateKeyLen];

CryptoBox.GenerateKeypair(senderPk, senderSk);
CryptoBox.GenerateKeypair(recipientPk, recipientSk);

var message = Encoding.UTF8.GetBytes("Top secret");
var ciphertext = new byte[message.Length + CryptoBox.MacLen + CryptoBox.NonceLen];

CryptoBox.EncryptWithKeypair(ciphertext, message, recipientPk, senderSk);

var decrypted = new byte[message.Length];
CryptoBox.DecryptWithKeypair(decrypted, ciphertext, senderPk, recipientSk);

Console.WriteLine($"It works: {decrypted.SequenceEqual(message)}");
```
