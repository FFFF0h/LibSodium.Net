using LibSodium.Interop;

namespace LibSodium;

/// <summary>
/// Provides SHAKE128 hashes whose output can have any requested length.
/// </summary>
public static class CryptoShake128
{
    /// <summary>Natural processing block length of SHAKE128 in bytes.</summary>
    public const int BlockLen = Native.CRYPTO_XOF_SHAKE128_BLOCKBYTES;

    /// <summary>Standard marker byte used to identify SHAKE input.</summary>
    public const byte StandardDomain = Native.CRYPTO_XOF_DOMAIN_STANDARD;

    /// <summary>Creates SHAKE128 output from an input message.</summary>
    /// <param name="output">A non-empty buffer that receives the requested number of output bytes.</param>
    /// <param name="input">The message to process.</param>
    /// <exception cref="ArgumentException"><paramref name="output"/> is empty.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the output.</exception>
    public static void Compute(Span<byte> output, ReadOnlySpan<byte> input) => CryptoXof<LowLevel.Shake128>.Compute(output, input);

    /// <summary>Creates a SHAKE128 operation that accepts input in several parts.</summary>
    /// <returns>A new incremental SHAKE128 operation.</returns>
    /// <exception cref="LibSodiumException">libsodium could not initialize the operation.</exception>
    public static ICryptoXofOperation CreateIncremental() => CryptoXof<LowLevel.Shake128>.CreateIncremental();

    /// <summary>Creates a SHAKE128 operation that uses a custom marker byte to separate this use from other uses.</summary>
    /// <param name="domain">The custom marker byte.</param>
    /// <returns>A new incremental SHAKE128 operation.</returns>
    /// <exception cref="LibSodiumException">libsodium rejected the marker or could not initialize the operation.</exception>
    public static ICryptoXofOperation CreateIncremental(byte domain) => CryptoXof<LowLevel.Shake128>.CreateIncremental(domain);
}

/// <summary>
/// Provides SHAKE256 hashes whose output can have any requested length.
/// </summary>
public static class CryptoShake256
{
    /// <summary>Natural processing block length of SHAKE256 in bytes.</summary>
    public const int BlockLen = Native.CRYPTO_XOF_SHAKE256_BLOCKBYTES;

    /// <summary>Standard marker byte used to identify SHAKE input.</summary>
    public const byte StandardDomain = Native.CRYPTO_XOF_DOMAIN_STANDARD;

    /// <summary>Creates SHAKE256 output from an input message.</summary>
    /// <param name="output">A non-empty buffer that receives the requested number of output bytes.</param>
    /// <param name="input">The message to process.</param>
    /// <exception cref="ArgumentException"><paramref name="output"/> is empty.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the output.</exception>
    public static void Compute(Span<byte> output, ReadOnlySpan<byte> input) => CryptoXof<LowLevel.Shake256>.Compute(output, input);

    /// <summary>Creates a SHAKE256 operation that accepts input in several parts.</summary>
    /// <returns>A new incremental SHAKE256 operation.</returns>
    /// <exception cref="LibSodiumException">libsodium could not initialize the operation.</exception>
    public static ICryptoXofOperation CreateIncremental() => CryptoXof<LowLevel.Shake256>.CreateIncremental();

    /// <summary>Creates a SHAKE256 operation that uses a custom marker byte to separate this use from other uses.</summary>
    /// <param name="domain">The custom marker byte.</param>
    /// <returns>A new incremental SHAKE256 operation.</returns>
    /// <exception cref="LibSodiumException">libsodium rejected the marker or could not initialize the operation.</exception>
    public static ICryptoXofOperation CreateIncremental(byte domain) => CryptoXof<LowLevel.Shake256>.CreateIncremental(domain);
}

/// <summary>
/// Provides TurboSHAKE128 hashes whose output can have any requested length.
/// </summary>
public static class CryptoTurboShake128
{
    /// <summary>Natural processing block length of TurboSHAKE128 in bytes.</summary>
    public const int BlockLen = Native.CRYPTO_XOF_SHAKE128_BLOCKBYTES;

    /// <summary>Standard marker byte used to identify TurboSHAKE input.</summary>
    public const byte StandardDomain = Native.CRYPTO_XOF_DOMAIN_STANDARD;

    /// <summary>Creates TurboSHAKE128 output from an input message.</summary>
    /// <param name="output">A non-empty buffer that receives the requested number of output bytes.</param>
    /// <param name="input">The message to process.</param>
    /// <exception cref="ArgumentException"><paramref name="output"/> is empty.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the output.</exception>
    public static void Compute(Span<byte> output, ReadOnlySpan<byte> input) => CryptoXof<LowLevel.TurboShake128>.Compute(output, input);

    /// <summary>Creates a TurboSHAKE128 operation that accepts input in several parts.</summary>
    /// <returns>A new incremental TurboSHAKE128 operation.</returns>
    /// <exception cref="LibSodiumException">libsodium could not initialize the operation.</exception>
    public static ICryptoXofOperation CreateIncremental() => CryptoXof<LowLevel.TurboShake128>.CreateIncremental();

    /// <summary>Creates a TurboSHAKE128 operation that uses a custom marker byte to separate this use from other uses.</summary>
    /// <param name="domain">The custom marker byte.</param>
    /// <returns>A new incremental TurboSHAKE128 operation.</returns>
    /// <exception cref="LibSodiumException">libsodium rejected the marker or could not initialize the operation.</exception>
    public static ICryptoXofOperation CreateIncremental(byte domain) => CryptoXof<LowLevel.TurboShake128>.CreateIncremental(domain);
}

/// <summary>
/// Provides TurboSHAKE256 hashes whose output can have any requested length.
/// </summary>
public static class CryptoTurboShake256
{
    /// <summary>Natural processing block length of TurboSHAKE256 in bytes.</summary>
    public const int BlockLen = Native.CRYPTO_XOF_SHAKE256_BLOCKBYTES;

    /// <summary>Standard marker byte used to identify TurboSHAKE input.</summary>
    public const byte StandardDomain = Native.CRYPTO_XOF_DOMAIN_STANDARD;

    /// <summary>Creates TurboSHAKE256 output from an input message.</summary>
    /// <param name="output">A non-empty buffer that receives the requested number of output bytes.</param>
    /// <param name="input">The message to process.</param>
    /// <exception cref="ArgumentException"><paramref name="output"/> is empty.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the output.</exception>
    public static void Compute(Span<byte> output, ReadOnlySpan<byte> input) => CryptoXof<LowLevel.TurboShake256>.Compute(output, input);

    /// <summary>Creates a TurboSHAKE256 operation that accepts input in several parts.</summary>
    /// <returns>A new incremental TurboSHAKE256 operation.</returns>
    /// <exception cref="LibSodiumException">libsodium could not initialize the operation.</exception>
    public static ICryptoXofOperation CreateIncremental() => CryptoXof<LowLevel.TurboShake256>.CreateIncremental();

    /// <summary>Creates a TurboSHAKE256 operation that uses a custom marker byte to separate this use from other uses.</summary>
    /// <param name="domain">The custom marker byte.</param>
    /// <returns>A new incremental TurboSHAKE256 operation.</returns>
    /// <exception cref="LibSodiumException">libsodium rejected the marker or could not initialize the operation.</exception>
    public static ICryptoXofOperation CreateIncremental(byte domain) => CryptoXof<LowLevel.TurboShake256>.CreateIncremental(domain);
}
