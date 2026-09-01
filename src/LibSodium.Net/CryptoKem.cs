using LibSodium.LowLevel;

namespace LibSodium;

/// <summary>Provides shared input checks and error handling for a key encapsulation algorithm.</summary>
/// <typeparam name="T">The low-level algorithm to use.</typeparam>
internal static class CryptoKemCore<T> where T : IKem
{
    /// <summary>Creates a new random key pair.</summary>
    /// <param name="publicKey">The buffer that receives the public key.</param>
    /// <param name="secretKey">The buffer that receives the secret key.</param>
    /// <exception cref="ArgumentException">A buffer does not have the length required by the algorithm.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the key pair.</exception>
    public static void GenerateKeyPair(Span<byte> publicKey, Span<byte> secretKey)
    {
        Validate(publicKey, T.PublicKeyLen, nameof(publicKey));
        Validate(secretKey, T.SecretKeyLen, nameof(secretKey));
        LibraryInitializer.EnsureInitialized();
        ThrowIfFailed(T.GenerateKeyPair(publicKey, secretKey), "KEM key pair generation failed.");
    }

    /// <summary>Creates the same key pair each time the same seed is provided.</summary>
    /// <param name="publicKey">The buffer that receives the public key.</param>
    /// <param name="secretKey">The buffer that receives the secret key.</param>
    /// <param name="seed">The seed used to create the key pair.</param>
    /// <exception cref="ArgumentException">A buffer does not have the length required by the algorithm.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the key pair.</exception>
    public static void GenerateKeyPairDeterministically(Span<byte> publicKey, Span<byte> secretKey, ReadOnlySpan<byte> seed)
    {
        Validate(publicKey, T.PublicKeyLen, nameof(publicKey));
        Validate(secretKey, T.SecretKeyLen, nameof(secretKey));
        Validate(seed, T.SeedLen, nameof(seed));
        LibraryInitializer.EnsureInitialized();
        ThrowIfFailed(T.GenerateKeyPair(publicKey, secretKey, seed), "Deterministic KEM key pair generation failed.");
    }

    /// <summary>Creates a ciphertext and shared secret for a recipient.</summary>
    /// <param name="ciphertext">The buffer that receives the ciphertext.</param>
    /// <param name="sharedSecret">The buffer that receives the shared secret.</param>
    /// <param name="publicKey">The recipient's public key.</param>
    /// <exception cref="ArgumentException">A buffer does not have the length required by the algorithm.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the ciphertext and shared secret.</exception>
    public static void Encapsulate(Span<byte> ciphertext, Span<byte> sharedSecret, ReadOnlySpan<byte> publicKey)
    {
        Validate(ciphertext, T.CiphertextLen, nameof(ciphertext));
        Validate(sharedSecret, T.SharedSecretLen, nameof(sharedSecret));
        Validate(publicKey, T.PublicKeyLen, nameof(publicKey));
        LibraryInitializer.EnsureInitialized();
        ThrowIfFailed(T.Encapsulate(ciphertext, sharedSecret, publicKey), "KEM encapsulation failed.");
    }

    /// <summary>Creates the same ciphertext and shared secret each time the same inputs are provided.</summary>
    /// <param name="ciphertext">The buffer that receives the ciphertext.</param>
    /// <param name="sharedSecret">The buffer that receives the shared secret.</param>
    /// <param name="publicKey">The recipient's public key.</param>
    /// <param name="seed">The seed used for encapsulation.</param>
    /// <exception cref="ArgumentException">A buffer does not have the length required by the algorithm.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the ciphertext and shared secret.</exception>
    public static void EncapsulateDeterministically(Span<byte> ciphertext, Span<byte> sharedSecret, ReadOnlySpan<byte> publicKey, ReadOnlySpan<byte> seed)
    {
        Validate(ciphertext, T.CiphertextLen, nameof(ciphertext));
        Validate(sharedSecret, T.SharedSecretLen, nameof(sharedSecret));
        Validate(publicKey, T.PublicKeyLen, nameof(publicKey));
        Validate(seed, T.EncapsulationSeedLen, nameof(seed));
        LibraryInitializer.EnsureInitialized();
        ThrowIfFailed(T.Encapsulate(ciphertext, sharedSecret, publicKey, seed), "Deterministic KEM encapsulation failed.");
    }

    /// <summary>Creates a shared secret from a ciphertext and the recipient's secret key.</summary>
    /// <param name="sharedSecret">The buffer that receives the shared secret.</param>
    /// <param name="ciphertext">The ciphertext received from the sender.</param>
    /// <param name="secretKey">The recipient's secret key.</param>
    /// <exception cref="ArgumentException">A buffer does not have the length required by the algorithm.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the shared secret.</exception>
    public static void Decapsulate(Span<byte> sharedSecret, ReadOnlySpan<byte> ciphertext, ReadOnlySpan<byte> secretKey)
    {
        Validate(sharedSecret, T.SharedSecretLen, nameof(sharedSecret));
        Validate(ciphertext, T.CiphertextLen, nameof(ciphertext));
        Validate(secretKey, T.SecretKeyLen, nameof(secretKey));
        LibraryInitializer.EnsureInitialized();
        ThrowIfFailed(T.Decapsulate(sharedSecret, ciphertext, secretKey), "KEM decapsulation failed.");
    }

    private static void Validate(ReadOnlySpan<byte> value, int expectedLength, string parameterName)
    {
        if (value.Length != expectedLength)
            throw new ArgumentException($"Buffer must be exactly {expectedLength} bytes.", parameterName);
    }

    private static void ThrowIfFailed(int result, string message)
    {
        if (result != 0)
            throw new LibSodiumException(message);
    }
}

/// <summary>
/// Provides the recommended X-Wing method for creating a shared secret between a sender and a recipient.
/// </summary>
/// <remarks>
/// The recipient creates a key pair and shares the public key. The sender calls <see cref="Encapsulate"/>
/// to create a ciphertext and a shared secret. The recipient calls <see cref="Decapsulate"/> with the
/// ciphertext and secret key to create the same shared secret.
/// </remarks>
public static class CryptoKem
{
    /// <summary>Length of a public key in bytes.</summary>
    public const int PublicKeyLen = 1216;

    /// <summary>Length of a secret key in bytes.</summary>
    public const int SecretKeyLen = 32;

    /// <summary>Length of an encapsulated ciphertext in bytes.</summary>
    public const int CiphertextLen = 1120;

    /// <summary>Length of the shared secret in bytes.</summary>
    public const int SharedSecretLen = 32;

    /// <summary>Length of a seed used to create a key pair in bytes.</summary>
    public const int SeedLen = 32;

    /// <summary>Name of the libsodium algorithm used by this class.</summary>
    public const string Primitive = "xwing";

    /// <summary>Creates a new random X-Wing key pair.</summary>
    /// <param name="publicKey">The buffer that receives the public key. It must be exactly <see cref="PublicKeyLen"/> bytes.</param>
    /// <param name="secretKey">The buffer that receives the secret key. It must be exactly <see cref="SecretKeyLen"/> bytes.</param>
    /// <exception cref="ArgumentException">A buffer does not have the required length.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the key pair.</exception>
    public static void GenerateKeyPair(Span<byte> publicKey, Span<byte> secretKey) => CryptoKemCore<DefaultKem>.GenerateKeyPair(publicKey, secretKey);

    /// <summary>Creates the same X-Wing key pair each time the same seed is provided.</summary>
    /// <param name="publicKey">The buffer that receives the public key. It must be exactly <see cref="PublicKeyLen"/> bytes.</param>
    /// <param name="secretKey">The buffer that receives the secret key. It must be exactly <see cref="SecretKeyLen"/> bytes.</param>
    /// <param name="seed">The seed used to create the key pair. It must be exactly <see cref="SeedLen"/> bytes.</param>
    /// <exception cref="ArgumentException">A buffer does not have the required length.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the key pair.</exception>
    public static void GenerateKeyPairDeterministically(Span<byte> publicKey, Span<byte> secretKey, ReadOnlySpan<byte> seed) => CryptoKemCore<DefaultKem>.GenerateKeyPairDeterministically(publicKey, secretKey, seed);

    /// <summary>Creates a ciphertext and shared secret for a recipient.</summary>
    /// <param name="ciphertext">The buffer that receives the ciphertext. It must be exactly <see cref="CiphertextLen"/> bytes.</param>
    /// <param name="sharedSecret">The buffer that receives the shared secret. It must be exactly <see cref="SharedSecretLen"/> bytes.</param>
    /// <param name="publicKey">The recipient's public key. It must be exactly <see cref="PublicKeyLen"/> bytes.</param>
    /// <exception cref="ArgumentException">A buffer does not have the required length.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the ciphertext and shared secret.</exception>
    public static void Encapsulate(Span<byte> ciphertext, Span<byte> sharedSecret, ReadOnlySpan<byte> publicKey) => CryptoKemCore<DefaultKem>.Encapsulate(ciphertext, sharedSecret, publicKey);

    /// <summary>Creates the shared secret stored in an X-Wing ciphertext.</summary>
    /// <param name="sharedSecret">The buffer that receives the shared secret. It must be exactly <see cref="SharedSecretLen"/> bytes.</param>
    /// <param name="ciphertext">The ciphertext received from the sender. It must be exactly <see cref="CiphertextLen"/> bytes.</param>
    /// <param name="secretKey">The recipient's secret key. It must be exactly <see cref="SecretKeyLen"/> bytes.</param>
    /// <exception cref="ArgumentException">A buffer does not have the required length.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the shared secret.</exception>
    public static void Decapsulate(Span<byte> sharedSecret, ReadOnlySpan<byte> ciphertext, ReadOnlySpan<byte> secretKey) => CryptoKemCore<DefaultKem>.Decapsulate(sharedSecret, ciphertext, secretKey);
}

/// <summary>
/// Provides X-Wing operations, including an option to create deterministic test data.
/// </summary>
/// <remarks>
/// Use <see cref="CryptoKem"/> for normal application code. This class also exposes deterministic
/// encapsulation, which is useful for repeatable tests and published test values.
/// </remarks>
public static class CryptoKemXWing
{
    /// <summary>Length of a public key in bytes.</summary>
    public const int PublicKeyLen = 1216;

    /// <summary>Length of a secret key in bytes.</summary>
    public const int SecretKeyLen = 32;

    /// <summary>Length of an encapsulated ciphertext in bytes.</summary>
    public const int CiphertextLen = 1120;

    /// <summary>Length of the shared secret in bytes.</summary>
    public const int SharedSecretLen = 32;

    /// <summary>Length of a seed used to create a key pair in bytes.</summary>
    public const int SeedLen = 32;

    /// <summary>Length of a seed used for deterministic encapsulation in bytes.</summary>
    public const int EncapsulationSeedLen = 64;

    /// <summary>Creates a new random X-Wing key pair.</summary>
    /// <param name="publicKey">The buffer that receives the public key. It must be exactly <see cref="PublicKeyLen"/> bytes.</param>
    /// <param name="secretKey">The buffer that receives the secret key. It must be exactly <see cref="SecretKeyLen"/> bytes.</param>
    /// <exception cref="ArgumentException">A buffer does not have the required length.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the key pair.</exception>
    public static void GenerateKeyPair(Span<byte> publicKey, Span<byte> secretKey) => CryptoKemCore<XWing>.GenerateKeyPair(publicKey, secretKey);

    /// <summary>Creates the same X-Wing key pair each time the same seed is provided.</summary>
    /// <param name="publicKey">The buffer that receives the public key. It must be exactly <see cref="PublicKeyLen"/> bytes.</param>
    /// <param name="secretKey">The buffer that receives the secret key. It must be exactly <see cref="SecretKeyLen"/> bytes.</param>
    /// <param name="seed">The seed used to create the key pair. It must be exactly <see cref="SeedLen"/> bytes.</param>
    /// <exception cref="ArgumentException">A buffer does not have the required length.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the key pair.</exception>
    public static void GenerateKeyPairDeterministically(Span<byte> publicKey, Span<byte> secretKey, ReadOnlySpan<byte> seed) => CryptoKemCore<XWing>.GenerateKeyPairDeterministically(publicKey, secretKey, seed);

    /// <summary>Creates a ciphertext and shared secret for a recipient.</summary>
    /// <param name="ciphertext">The buffer that receives the ciphertext. It must be exactly <see cref="CiphertextLen"/> bytes.</param>
    /// <param name="sharedSecret">The buffer that receives the shared secret. It must be exactly <see cref="SharedSecretLen"/> bytes.</param>
    /// <param name="publicKey">The recipient's public key. It must be exactly <see cref="PublicKeyLen"/> bytes.</param>
    /// <exception cref="ArgumentException">A buffer does not have the required length.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the ciphertext and shared secret.</exception>
    public static void Encapsulate(Span<byte> ciphertext, Span<byte> sharedSecret, ReadOnlySpan<byte> publicKey) => CryptoKemCore<XWing>.Encapsulate(ciphertext, sharedSecret, publicKey);

    /// <summary>Creates the same ciphertext and shared secret each time the same inputs are provided.</summary>
    /// <param name="ciphertext">The buffer that receives the ciphertext. It must be exactly <see cref="CiphertextLen"/> bytes.</param>
    /// <param name="sharedSecret">The buffer that receives the shared secret. It must be exactly <see cref="SharedSecretLen"/> bytes.</param>
    /// <param name="publicKey">The recipient's public key. It must be exactly <see cref="PublicKeyLen"/> bytes.</param>
    /// <param name="seed">The seed used for encapsulation. It must be exactly <see cref="EncapsulationSeedLen"/> bytes.</param>
    /// <exception cref="ArgumentException">A buffer does not have the required length.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the ciphertext and shared secret.</exception>
    public static void EncapsulateDeterministically(Span<byte> ciphertext, Span<byte> sharedSecret, ReadOnlySpan<byte> publicKey, ReadOnlySpan<byte> seed) => CryptoKemCore<XWing>.EncapsulateDeterministically(ciphertext, sharedSecret, publicKey, seed);

    /// <summary>Creates the shared secret stored in an X-Wing ciphertext.</summary>
    /// <param name="sharedSecret">The buffer that receives the shared secret. It must be exactly <see cref="SharedSecretLen"/> bytes.</param>
    /// <param name="ciphertext">The ciphertext received from the sender. It must be exactly <see cref="CiphertextLen"/> bytes.</param>
    /// <param name="secretKey">The recipient's secret key. It must be exactly <see cref="SecretKeyLen"/> bytes.</param>
    /// <exception cref="ArgumentException">A buffer does not have the required length.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the shared secret.</exception>
    public static void Decapsulate(Span<byte> sharedSecret, ReadOnlySpan<byte> ciphertext, ReadOnlySpan<byte> secretKey) => CryptoKemCore<XWing>.Decapsulate(sharedSecret, ciphertext, secretKey);
}

/// <summary>
/// Provides ML-KEM768 operations for creating a shared secret between a sender and a recipient.
/// </summary>
/// <remarks>
/// <see cref="CryptoKem"/> is the recommended choice for most applications because it combines
/// ML-KEM768 with X25519. Use this class when ML-KEM768 is specifically required.
/// </remarks>
public static class CryptoKemMlKem768
{
    /// <summary>Length of a public key in bytes.</summary>
    public const int PublicKeyLen = 1184;

    /// <summary>Length of a secret key in bytes.</summary>
    public const int SecretKeyLen = 2400;

    /// <summary>Length of an encapsulated ciphertext in bytes.</summary>
    public const int CiphertextLen = 1088;

    /// <summary>Length of the shared secret in bytes.</summary>
    public const int SharedSecretLen = 32;

    /// <summary>Length of a seed used to create a key pair in bytes.</summary>
    public const int SeedLen = 64;

    /// <summary>Length of a seed used for deterministic encapsulation in bytes.</summary>
    public const int EncapsulationSeedLen = 32;

    /// <summary>Creates a new random ML-KEM768 key pair.</summary>
    /// <param name="publicKey">The buffer that receives the public key. It must be exactly <see cref="PublicKeyLen"/> bytes.</param>
    /// <param name="secretKey">The buffer that receives the secret key. It must be exactly <see cref="SecretKeyLen"/> bytes.</param>
    /// <exception cref="ArgumentException">A buffer does not have the required length.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the key pair.</exception>
    public static void GenerateKeyPair(Span<byte> publicKey, Span<byte> secretKey) => CryptoKemCore<MlKem768>.GenerateKeyPair(publicKey, secretKey);

    /// <summary>Creates the same ML-KEM768 key pair each time the same seed is provided.</summary>
    /// <param name="publicKey">The buffer that receives the public key. It must be exactly <see cref="PublicKeyLen"/> bytes.</param>
    /// <param name="secretKey">The buffer that receives the secret key. It must be exactly <see cref="SecretKeyLen"/> bytes.</param>
    /// <param name="seed">The seed used to create the key pair. It must be exactly <see cref="SeedLen"/> bytes.</param>
    /// <exception cref="ArgumentException">A buffer does not have the required length.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the key pair.</exception>
    public static void GenerateKeyPairDeterministically(Span<byte> publicKey, Span<byte> secretKey, ReadOnlySpan<byte> seed) => CryptoKemCore<MlKem768>.GenerateKeyPairDeterministically(publicKey, secretKey, seed);

    /// <summary>Creates a ciphertext and shared secret for a recipient.</summary>
    /// <param name="ciphertext">The buffer that receives the ciphertext. It must be exactly <see cref="CiphertextLen"/> bytes.</param>
    /// <param name="sharedSecret">The buffer that receives the shared secret. It must be exactly <see cref="SharedSecretLen"/> bytes.</param>
    /// <param name="publicKey">The recipient's public key. It must be exactly <see cref="PublicKeyLen"/> bytes.</param>
    /// <exception cref="ArgumentException">A buffer does not have the required length.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the ciphertext and shared secret.</exception>
    public static void Encapsulate(Span<byte> ciphertext, Span<byte> sharedSecret, ReadOnlySpan<byte> publicKey) => CryptoKemCore<MlKem768>.Encapsulate(ciphertext, sharedSecret, publicKey);

    /// <summary>Creates the same ciphertext and shared secret each time the same inputs are provided.</summary>
    /// <param name="ciphertext">The buffer that receives the ciphertext. It must be exactly <see cref="CiphertextLen"/> bytes.</param>
    /// <param name="sharedSecret">The buffer that receives the shared secret. It must be exactly <see cref="SharedSecretLen"/> bytes.</param>
    /// <param name="publicKey">The recipient's public key. It must be exactly <see cref="PublicKeyLen"/> bytes.</param>
    /// <param name="seed">The seed used for encapsulation. It must be exactly <see cref="EncapsulationSeedLen"/> bytes.</param>
    /// <exception cref="ArgumentException">A buffer does not have the required length.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the ciphertext and shared secret.</exception>
    public static void EncapsulateDeterministically(Span<byte> ciphertext, Span<byte> sharedSecret, ReadOnlySpan<byte> publicKey, ReadOnlySpan<byte> seed) => CryptoKemCore<MlKem768>.EncapsulateDeterministically(ciphertext, sharedSecret, publicKey, seed);

    /// <summary>Creates the shared secret stored in an ML-KEM768 ciphertext.</summary>
    /// <param name="sharedSecret">The buffer that receives the shared secret. It must be exactly <see cref="SharedSecretLen"/> bytes.</param>
    /// <param name="ciphertext">The ciphertext received from the sender. It must be exactly <see cref="CiphertextLen"/> bytes.</param>
    /// <param name="secretKey">The recipient's secret key. It must be exactly <see cref="SecretKeyLen"/> bytes.</param>
    /// <exception cref="ArgumentException">A buffer does not have the required length.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the shared secret.</exception>
    public static void Decapsulate(Span<byte> sharedSecret, ReadOnlySpan<byte> ciphertext, ReadOnlySpan<byte> secretKey) => CryptoKemCore<MlKem768>.Decapsulate(sharedSecret, ciphertext, secretKey);
}
