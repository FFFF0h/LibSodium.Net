using LibSodium.Interop;

namespace LibSodium.LowLevel;

/// <summary>Defines the low-level operations and sizes required by a key encapsulation algorithm.</summary>
internal interface IKem
{
    /// <summary>Gets the public key length in bytes.</summary>
    /// <value>The required public key length.</value>
    static abstract int PublicKeyLen { get; }
    /// <summary>Gets the secret key length in bytes.</summary>
    /// <value>The required secret key length.</value>
    static abstract int SecretKeyLen { get; }
    /// <summary>Gets the ciphertext length in bytes.</summary>
    /// <value>The required ciphertext length.</value>
    static abstract int CiphertextLen { get; }
    /// <summary>Gets the shared secret length in bytes.</summary>
    /// <value>The required shared secret length.</value>
    static abstract int SharedSecretLen { get; }
    /// <summary>Gets the key-pair seed length in bytes.</summary>
    /// <value>The required key-pair seed length.</value>
    static abstract int SeedLen { get; }
    /// <summary>Gets the deterministic encapsulation seed length in bytes.</summary>
    /// <value>The required encapsulation seed length.</value>
    static abstract int EncapsulationSeedLen { get; }
    /// <summary>Creates a random key pair.</summary>
    /// <param name="publicKey">The buffer that receives the public key.</param>
    /// <param name="secretKey">The buffer that receives the secret key.</param>
    /// <returns>Zero on success; otherwise, a nonzero error code.</returns>
    static abstract int GenerateKeyPair(Span<byte> publicKey, Span<byte> secretKey);
    /// <summary>Creates a key pair from a seed.</summary>
    /// <param name="publicKey">The buffer that receives the public key.</param>
    /// <param name="secretKey">The buffer that receives the secret key.</param>
    /// <param name="seed">The seed used to create the key pair.</param>
    /// <returns>Zero on success; otherwise, a nonzero error code.</returns>
    static abstract int GenerateKeyPair(Span<byte> publicKey, Span<byte> secretKey, ReadOnlySpan<byte> seed);
    /// <summary>Creates a ciphertext and shared secret.</summary>
    /// <param name="ciphertext">The buffer that receives the ciphertext.</param>
    /// <param name="sharedSecret">The buffer that receives the shared secret.</param>
    /// <param name="publicKey">The recipient's public key.</param>
    /// <returns>Zero on success; otherwise, a nonzero error code.</returns>
    static abstract int Encapsulate(Span<byte> ciphertext, Span<byte> sharedSecret, ReadOnlySpan<byte> publicKey);
    /// <summary>Creates a ciphertext and shared secret from a seed.</summary>
    /// <param name="ciphertext">The buffer that receives the ciphertext.</param>
    /// <param name="sharedSecret">The buffer that receives the shared secret.</param>
    /// <param name="publicKey">The recipient's public key.</param>
    /// <param name="seed">The seed used for encapsulation.</param>
    /// <returns>Zero on success; otherwise, a nonzero error code.</returns>
    static abstract int Encapsulate(Span<byte> ciphertext, Span<byte> sharedSecret, ReadOnlySpan<byte> publicKey, ReadOnlySpan<byte> seed);
    /// <summary>Creates a shared secret from a ciphertext and secret key.</summary>
    /// <param name="sharedSecret">The buffer that receives the shared secret.</param>
    /// <param name="ciphertext">The ciphertext received from the sender.</param>
    /// <param name="secretKey">The recipient's secret key.</param>
    /// <returns>Zero on success; otherwise, a nonzero error code.</returns>
    static abstract int Decapsulate(Span<byte> sharedSecret, ReadOnlySpan<byte> ciphertext, ReadOnlySpan<byte> secretKey);
}

/// <summary>Connects the default libsodium key encapsulation functions to the shared wrapper.</summary>
internal readonly struct DefaultKem : IKem
{
    /// <inheritdoc />
    public static int PublicKeyLen => 1216;
    /// <inheritdoc />
    public static int SecretKeyLen => 32;
    /// <inheritdoc />
    public static int CiphertextLen => 1120;
    /// <inheritdoc />
    public static int SharedSecretLen => 32;
    /// <inheritdoc />
    public static int SeedLen => 32;
    /// <inheritdoc />
    public static int EncapsulationSeedLen => 64;
    /// <inheritdoc />
    public static int GenerateKeyPair(Span<byte> publicKey, Span<byte> secretKey) => Native.crypto_kem_keypair(publicKey, secretKey);
    /// <inheritdoc />
    public static int GenerateKeyPair(Span<byte> publicKey, Span<byte> secretKey, ReadOnlySpan<byte> seed) => Native.crypto_kem_seed_keypair(publicKey, secretKey, seed);
    /// <inheritdoc />
    public static int Encapsulate(Span<byte> ciphertext, Span<byte> sharedSecret, ReadOnlySpan<byte> publicKey) => Native.crypto_kem_enc(ciphertext, sharedSecret, publicKey);
    /// <inheritdoc />
    public static int Encapsulate(Span<byte> ciphertext, Span<byte> sharedSecret, ReadOnlySpan<byte> publicKey, ReadOnlySpan<byte> seed) => Native.crypto_kem_xwing_enc_deterministic(ciphertext, sharedSecret, publicKey, seed);
    /// <inheritdoc />
    public static int Decapsulate(Span<byte> sharedSecret, ReadOnlySpan<byte> ciphertext, ReadOnlySpan<byte> secretKey) => Native.crypto_kem_dec(sharedSecret, ciphertext, secretKey);
}

/// <summary>Connects the X-Wing functions to the shared wrapper.</summary>
internal readonly struct XWing : IKem
{
    /// <inheritdoc />
    public static int PublicKeyLen => 1216;
    /// <inheritdoc />
    public static int SecretKeyLen => 32;
    /// <inheritdoc />
    public static int CiphertextLen => 1120;
    /// <inheritdoc />
    public static int SharedSecretLen => 32;
    /// <inheritdoc />
    public static int SeedLen => 32;
    /// <inheritdoc />
    public static int EncapsulationSeedLen => 64;
    /// <inheritdoc />
    public static int GenerateKeyPair(Span<byte> publicKey, Span<byte> secretKey) => Native.crypto_kem_xwing_keypair(publicKey, secretKey);
    /// <inheritdoc />
    public static int GenerateKeyPair(Span<byte> publicKey, Span<byte> secretKey, ReadOnlySpan<byte> seed) => Native.crypto_kem_xwing_seed_keypair(publicKey, secretKey, seed);
    /// <inheritdoc />
    public static int Encapsulate(Span<byte> ciphertext, Span<byte> sharedSecret, ReadOnlySpan<byte> publicKey) => Native.crypto_kem_xwing_enc(ciphertext, sharedSecret, publicKey);
    /// <inheritdoc />
    public static int Encapsulate(Span<byte> ciphertext, Span<byte> sharedSecret, ReadOnlySpan<byte> publicKey, ReadOnlySpan<byte> seed) => Native.crypto_kem_xwing_enc_deterministic(ciphertext, sharedSecret, publicKey, seed);
    /// <inheritdoc />
    public static int Decapsulate(Span<byte> sharedSecret, ReadOnlySpan<byte> ciphertext, ReadOnlySpan<byte> secretKey) => Native.crypto_kem_xwing_dec(sharedSecret, ciphertext, secretKey);
}

/// <summary>Connects the ML-KEM768 functions to the shared wrapper.</summary>
internal readonly struct MlKem768 : IKem
{
    /// <inheritdoc />
    public static int PublicKeyLen => 1184;
    /// <inheritdoc />
    public static int SecretKeyLen => 2400;
    /// <inheritdoc />
    public static int CiphertextLen => 1088;
    /// <inheritdoc />
    public static int SharedSecretLen => 32;
    /// <inheritdoc />
    public static int SeedLen => 64;
    /// <inheritdoc />
    public static int EncapsulationSeedLen => 32;
    /// <inheritdoc />
    public static int GenerateKeyPair(Span<byte> publicKey, Span<byte> secretKey) => Native.crypto_kem_mlkem768_keypair(publicKey, secretKey);
    /// <inheritdoc />
    public static int GenerateKeyPair(Span<byte> publicKey, Span<byte> secretKey, ReadOnlySpan<byte> seed) => Native.crypto_kem_mlkem768_seed_keypair(publicKey, secretKey, seed);
    /// <inheritdoc />
    public static int Encapsulate(Span<byte> ciphertext, Span<byte> sharedSecret, ReadOnlySpan<byte> publicKey) => Native.crypto_kem_mlkem768_enc(ciphertext, sharedSecret, publicKey);
    /// <inheritdoc />
    public static int Encapsulate(Span<byte> ciphertext, Span<byte> sharedSecret, ReadOnlySpan<byte> publicKey, ReadOnlySpan<byte> seed) => Native.crypto_kem_mlkem768_enc_deterministic(ciphertext, sharedSecret, publicKey, seed);
    /// <inheritdoc />
    public static int Decapsulate(Span<byte> sharedSecret, ReadOnlySpan<byte> ciphertext, ReadOnlySpan<byte> secretKey) => Native.crypto_kem_mlkem768_dec(sharedSecret, ciphertext, secretKey);
}
