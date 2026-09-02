# Features

LibSodium.Net targets .NET 10 and binds to libsodium 1.0.22. It uses source-generated P/Invoke for Native AOT compatibility and span-based APIs for efficient buffer handling.

## Authenticated Encryption

- `SecretBox` authenticated encryption
- `SecretStream` authenticated streaming and file encryption
- XChaCha20-Poly1305, ChaCha20-Poly1305, ChaCha20-Poly1305-IETF, AES256-GCM, AEGIS-128L, and AEGIS-256
- Combined and detached modes with optional AAD and automatic or manual nonce handling

## Public-Key Cryptography

- `CryptoBox` authenticated encryption and sealed boxes
- Ed25519 signatures and Ed25519ph streaming signatures
- X25519 key exchange and scalar multiplication
- Ristretto255 group and scalar operations
- ML-KEM768 and X-Wing key encapsulation

## Hashing, Authentication, and Derivation

- BLAKE2b, SipHash, SHA-256, SHA-512, SHA3-256, and SHA3-512
- SHAKE128, SHAKE256, TurboSHAKE128, TurboSHAKE256, and Keccak-f[1600]
- HMAC-SHA-256, HMAC-SHA-512, HMAC-SHA-512/256, and Poly1305
- Argon2 and scrypt password hashing
- HKDF-SHA-256, HKDF-SHA-512, HChaCha20, and libsodium's BLAKE2b KDF

## Utilities

- ChaCha20, XChaCha20, Salsa20, and XSalsa20 stream ciphers
- IPcrypt deterministic, ND, NDX, and prefix-preserving IP address encryption
- Cryptographically secure random generation
- Hexadecimal and Base64 encoding
- ISO/IEC 7816-4 padding
- Constant-time comparisons and arbitrary-length unsigned integer operations
- Guarded secure memory with zeroing, locking, and access protection
