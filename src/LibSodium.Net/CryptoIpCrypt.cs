using LibSodium.Interop;

namespace LibSodium;

/// <summary>
/// Provides methods for encrypting and decrypting 16-byte IP addresses with IPcrypt.
/// </summary>
/// <remarks>
/// IPcrypt supports deterministic, non-deterministic (ND), extended non-deterministic (NDX),
/// and prefix-preserving modes. Each mode requires keys and buffers of specific sizes.
/// </remarks>
public static class CryptoIpCrypt
{
    /// <summary>Length of an IP address input in bytes.</summary>
    public const int InputLen = 16;

    /// <summary>Length of deterministic and prefix-preserving output in bytes.</summary>
    public const int OutputLen = 16;

    /// <summary>Length of a deterministic IPcrypt key in bytes.</summary>
    public const int KeyLen = 16;

    /// <summary>Length of an IPcrypt-ND key in bytes.</summary>
    public const int NdKeyLen = 16;

    /// <summary>Length of an IPcrypt-ND tweak in bytes.</summary>
    public const int NdTweakLen = 8;

    /// <summary>Length of an IPcrypt-ND encrypted value in bytes.</summary>
    public const int NdOutputLen = 24;

    /// <summary>Length of an IPcrypt-NDX key in bytes.</summary>
    public const int NdxKeyLen = 32;

    /// <summary>Length of an IPcrypt-NDX tweak in bytes.</summary>
    public const int NdxTweakLen = 16;

    /// <summary>Length of an IPcrypt-NDX encrypted value in bytes.</summary>
    public const int NdxOutputLen = 32;

    /// <summary>Length of a prefix-preserving IPcrypt key in bytes.</summary>
    public const int PrefixKeyLen = 32;

    /// <summary>Generates a random key for deterministic IPcrypt.</summary>
    /// <param name="key">The buffer that receives the generated key. It must be exactly <see cref="KeyLen"/> bytes.</param>
    /// <exception cref="ArgumentException"><paramref name="key"/> does not have the required length.</exception>
    public static void GenerateKey(Span<byte> key)
    {
        Validate(key, KeyLen, nameof(key));
        LibraryInitializer.EnsureInitialized();
        Native.crypto_ipcrypt_keygen(key);
    }

    /// <summary>Generates a random key for IPcrypt-ND.</summary>
    /// <param name="key">The buffer that receives the generated key. It must be exactly <see cref="NdKeyLen"/> bytes.</param>
    /// <exception cref="ArgumentException"><paramref name="key"/> does not have the required length.</exception>
    public static void GenerateNdKey(Span<byte> key)
    {
        Validate(key, NdKeyLen, nameof(key));
        LibraryInitializer.EnsureInitialized();
        Native.crypto_ipcrypt_nd_keygen(key);
    }

    /// <summary>Generates a random key for IPcrypt-NDX.</summary>
    /// <param name="key">The buffer that receives the generated key. It must be exactly <see cref="NdxKeyLen"/> bytes.</param>
    /// <exception cref="ArgumentException"><paramref name="key"/> does not have the required length.</exception>
    public static void GenerateNdxKey(Span<byte> key)
    {
        Validate(key, NdxKeyLen, nameof(key));
        LibraryInitializer.EnsureInitialized();
        Native.crypto_ipcrypt_ndx_keygen(key);
    }

    /// <summary>Generates a random key for prefix-preserving IPcrypt.</summary>
    /// <param name="key">The buffer that receives the generated key. It must be exactly <see cref="PrefixKeyLen"/> bytes.</param>
    /// <exception cref="ArgumentException"><paramref name="key"/> does not have the required length.</exception>
    public static void GeneratePrefixKey(Span<byte> key)
    {
        Validate(key, PrefixKeyLen, nameof(key));
        LibraryInitializer.EnsureInitialized();
        Native.crypto_ipcrypt_pfx_keygen(key);
    }

    /// <summary>Encrypts a 16-byte IP address so the same input and key always produce the same output.</summary>
    /// <param name="output">The buffer that receives the encrypted address. It must be exactly <see cref="OutputLen"/> bytes.</param>
    /// <param name="input">The address to encrypt. It must be exactly <see cref="InputLen"/> bytes.</param>
    /// <param name="key">The key used to encrypt the address. It must be exactly <see cref="KeyLen"/> bytes.</param>
    /// <exception cref="ArgumentException">A buffer does not have the required length.</exception>
    public static void Encrypt(Span<byte> output, ReadOnlySpan<byte> input, ReadOnlySpan<byte> key)
    {
        ValidateBasic(output, input, key);
        LibraryInitializer.EnsureInitialized();
        Native.crypto_ipcrypt_encrypt(output, input, key);
    }

    /// <summary>Decrypts an address that was encrypted with deterministic IPcrypt.</summary>
    /// <param name="output">The buffer that receives the decrypted address. It must be exactly <see cref="InputLen"/> bytes.</param>
    /// <param name="input">The encrypted address. It must be exactly <see cref="OutputLen"/> bytes.</param>
    /// <param name="key">The key used to decrypt the address. It must be exactly <see cref="KeyLen"/> bytes.</param>
    /// <exception cref="ArgumentException">A buffer does not have the required length.</exception>
    public static void Decrypt(Span<byte> output, ReadOnlySpan<byte> input, ReadOnlySpan<byte> key)
    {
        ValidateBasic(output, input, key);
        LibraryInitializer.EnsureInitialized();
        Native.crypto_ipcrypt_decrypt(output, input, key);
    }

    /// <summary>Encrypts a 16-byte IP address with IPcrypt-ND.</summary>
    /// <param name="output">The buffer that receives the encrypted value and its tweak. It must be exactly <see cref="NdOutputLen"/> bytes.</param>
    /// <param name="input">The address to encrypt. It must be exactly <see cref="InputLen"/> bytes.</param>
    /// <param name="tweak">A unique value for this encryption. It must be exactly <see cref="NdTweakLen"/> bytes.</param>
    /// <param name="key">The key used to encrypt the address. It must be exactly <see cref="NdKeyLen"/> bytes.</param>
    /// <exception cref="ArgumentException">A buffer does not have the required length.</exception>
    public static void EncryptNd(Span<byte> output, ReadOnlySpan<byte> input, ReadOnlySpan<byte> tweak, ReadOnlySpan<byte> key)
    {
        Validate(output, NdOutputLen, nameof(output));
        Validate(input, InputLen, nameof(input));
        Validate(tweak, NdTweakLen, nameof(tweak));
        Validate(key, NdKeyLen, nameof(key));
        LibraryInitializer.EnsureInitialized();
        Native.crypto_ipcrypt_nd_encrypt(output, input, tweak, key);
    }

    /// <summary>Decrypts a value that was encrypted with IPcrypt-ND.</summary>
    /// <param name="output">The buffer that receives the decrypted address. It must be exactly <see cref="InputLen"/> bytes.</param>
    /// <param name="input">The encrypted value. It must be exactly <see cref="NdOutputLen"/> bytes.</param>
    /// <param name="key">The key used to decrypt the value. It must be exactly <see cref="NdKeyLen"/> bytes.</param>
    /// <exception cref="ArgumentException">A buffer does not have the required length.</exception>
    public static void DecryptNd(Span<byte> output, ReadOnlySpan<byte> input, ReadOnlySpan<byte> key)
    {
        Validate(output, InputLen, nameof(output));
        Validate(input, NdOutputLen, nameof(input));
        Validate(key, NdKeyLen, nameof(key));
        LibraryInitializer.EnsureInitialized();
        Native.crypto_ipcrypt_nd_decrypt(output, input, key);
    }

    /// <summary>Encrypts a 16-byte IP address with IPcrypt-NDX.</summary>
    /// <param name="output">The buffer that receives the encrypted value and its tweak. It must be exactly <see cref="NdxOutputLen"/> bytes.</param>
    /// <param name="input">The address to encrypt. It must be exactly <see cref="InputLen"/> bytes.</param>
    /// <param name="tweak">A unique value for this encryption. It must be exactly <see cref="NdxTweakLen"/> bytes.</param>
    /// <param name="key">The key used to encrypt the address. It must be exactly <see cref="NdxKeyLen"/> bytes.</param>
    /// <exception cref="ArgumentException">A buffer does not have the required length.</exception>
    public static void EncryptNdx(Span<byte> output, ReadOnlySpan<byte> input, ReadOnlySpan<byte> tweak, ReadOnlySpan<byte> key)
    {
        Validate(output, NdxOutputLen, nameof(output));
        Validate(input, InputLen, nameof(input));
        Validate(tweak, NdxTweakLen, nameof(tweak));
        Validate(key, NdxKeyLen, nameof(key));
        LibraryInitializer.EnsureInitialized();
        Native.crypto_ipcrypt_ndx_encrypt(output, input, tweak, key);
    }

    /// <summary>Decrypts a value that was encrypted with IPcrypt-NDX.</summary>
    /// <param name="output">The buffer that receives the decrypted address. It must be exactly <see cref="InputLen"/> bytes.</param>
    /// <param name="input">The encrypted value. It must be exactly <see cref="NdxOutputLen"/> bytes.</param>
    /// <param name="key">The key used to decrypt the value. It must be exactly <see cref="NdxKeyLen"/> bytes.</param>
    /// <exception cref="ArgumentException">A buffer does not have the required length.</exception>
    public static void DecryptNdx(Span<byte> output, ReadOnlySpan<byte> input, ReadOnlySpan<byte> key)
    {
        Validate(output, InputLen, nameof(output));
        Validate(input, NdxOutputLen, nameof(input));
        Validate(key, NdxKeyLen, nameof(key));
        LibraryInitializer.EnsureInitialized();
        Native.crypto_ipcrypt_ndx_decrypt(output, input, key);
    }

    /// <summary>Encrypts a 16-byte IP address while keeping shared address prefixes recognizable.</summary>
    /// <param name="output">The buffer that receives the encrypted address. It must be exactly <see cref="OutputLen"/> bytes.</param>
    /// <param name="input">The address to encrypt. It must be exactly <see cref="InputLen"/> bytes.</param>
    /// <param name="key">The key used to encrypt the address. It must be exactly <see cref="PrefixKeyLen"/> bytes.</param>
    /// <exception cref="ArgumentException">A buffer does not have the required length.</exception>
    public static void EncryptPrefix(Span<byte> output, ReadOnlySpan<byte> input, ReadOnlySpan<byte> key)
    {
        ValidatePrefix(output, input, key);
        LibraryInitializer.EnsureInitialized();
        Native.crypto_ipcrypt_pfx_encrypt(output, input, key);
    }

    /// <summary>Decrypts an address that was encrypted with prefix-preserving IPcrypt.</summary>
    /// <param name="output">The buffer that receives the decrypted address. It must be exactly <see cref="InputLen"/> bytes.</param>
    /// <param name="input">The encrypted address. It must be exactly <see cref="OutputLen"/> bytes.</param>
    /// <param name="key">The key used to decrypt the address. It must be exactly <see cref="PrefixKeyLen"/> bytes.</param>
    /// <exception cref="ArgumentException">A buffer does not have the required length.</exception>
    public static void DecryptPrefix(Span<byte> output, ReadOnlySpan<byte> input, ReadOnlySpan<byte> key)
    {
        ValidatePrefix(output, input, key);
        LibraryInitializer.EnsureInitialized();
        Native.crypto_ipcrypt_pfx_decrypt(output, input, key);
    }

    private static void ValidateBasic(Span<byte> output, ReadOnlySpan<byte> input, ReadOnlySpan<byte> key)
    {
        Validate(output, OutputLen, nameof(output));
        Validate(input, InputLen, nameof(input));
        Validate(key, KeyLen, nameof(key));
    }

    private static void ValidatePrefix(Span<byte> output, ReadOnlySpan<byte> input, ReadOnlySpan<byte> key)
    {
        Validate(output, OutputLen, nameof(output));
        Validate(input, InputLen, nameof(input));
        Validate(key, PrefixKeyLen, nameof(key));
    }

    private static void Validate(ReadOnlySpan<byte> value, int expectedLength, string parameterName)
    {
        if (value.Length != expectedLength)
            throw new ArgumentException($"Buffer must be exactly {expectedLength} bytes.", parameterName);
    }
}
