using LibSodium.Interop;

namespace LibSodium;

/// <summary>
/// Provides methods for creating fixed-length SHA3-256 hashes with libsodium.
/// </summary>
public static class CryptoSha3256
{
    /// <summary>Length of a SHA3-256 hash in bytes.</summary>
    public const int HashLen = Native.CRYPTO_HASH_SHA3256_BYTES;

    /// <summary>Creates a SHA3-256 hash from a message.</summary>
    /// <param name="hash">The buffer that receives the hash. It must be exactly <see cref="HashLen"/> bytes.</param>
    /// <param name="message">The message to hash.</param>
    /// <exception cref="ArgumentException"><paramref name="hash"/> does not have the required length.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the hash.</exception>
    public static void ComputeHash(Span<byte> hash, ReadOnlySpan<byte> message) => CryptoKeyLessHash<LowLevel.Sha3256>.ComputeHash(hash, message);

    /// <summary>Creates a SHA3-256 hash from all remaining data in a stream.</summary>
    /// <param name="hash">The buffer that receives the hash. It must be exactly <see cref="HashLen"/> bytes.</param>
    /// <param name="input">The stream to read until its end.</param>
    /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="hash"/> does not have the required length.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the hash.</exception>
    public static void ComputeHash(Span<byte> hash, Stream input) => CryptoKeyLessHash<LowLevel.Sha3256>.ComputeHash(hash, input);

    /// <summary>Asynchronously creates a SHA3-256 hash from all remaining data in a stream.</summary>
    /// <param name="hash">The buffer that receives the hash. It must be exactly <see cref="HashLen"/> bytes.</param>
    /// <param name="input">The stream to read until its end.</param>
    /// <param name="cancellationToken">A token that can stop the operation.</param>
    /// <returns>A task that completes after the hash has been written to <paramref name="hash"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="hash"/> does not have the required length.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the hash.</exception>
    /// <exception cref="OperationCanceledException"><paramref name="cancellationToken"/> was canceled.</exception>
    public static Task ComputeHashAsync(Memory<byte> hash, Stream input, CancellationToken cancellationToken = default) => CryptoKeyLessHash<LowLevel.Sha3256>.ComputeHashAsync(hash, input, cancellationToken);

    /// <summary>Creates an operation that accepts a message in several parts.</summary>
    /// <returns>A new incremental SHA3-256 operation.</returns>
    /// <exception cref="LibSodiumException">libsodium could not initialize the operation.</exception>
    public static ICryptoIncrementalOperation CreateIncrementalHash() => new CryptoKeyLessHashIncremental<LowLevel.Sha3256>();
}

/// <summary>
/// Provides methods for creating fixed-length SHA3-512 hashes with libsodium.
/// </summary>
public static class CryptoSha3512
{
    /// <summary>Length of a SHA3-512 hash in bytes.</summary>
    public const int HashLen = Native.CRYPTO_HASH_SHA3512_BYTES;

    /// <summary>Creates a SHA3-512 hash from a message.</summary>
    /// <param name="hash">The buffer that receives the hash. It must be exactly <see cref="HashLen"/> bytes.</param>
    /// <param name="message">The message to hash.</param>
    /// <exception cref="ArgumentException"><paramref name="hash"/> does not have the required length.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the hash.</exception>
    public static void ComputeHash(Span<byte> hash, ReadOnlySpan<byte> message) => CryptoKeyLessHash<LowLevel.Sha3512>.ComputeHash(hash, message);

    /// <summary>Creates a SHA3-512 hash from all remaining data in a stream.</summary>
    /// <param name="hash">The buffer that receives the hash. It must be exactly <see cref="HashLen"/> bytes.</param>
    /// <param name="input">The stream to read until its end.</param>
    /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="hash"/> does not have the required length.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the hash.</exception>
    public static void ComputeHash(Span<byte> hash, Stream input) => CryptoKeyLessHash<LowLevel.Sha3512>.ComputeHash(hash, input);

    /// <summary>Asynchronously creates a SHA3-512 hash from all remaining data in a stream.</summary>
    /// <param name="hash">The buffer that receives the hash. It must be exactly <see cref="HashLen"/> bytes.</param>
    /// <param name="input">The stream to read until its end.</param>
    /// <param name="cancellationToken">A token that can stop the operation.</param>
    /// <returns>A task that completes after the hash has been written to <paramref name="hash"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="hash"/> does not have the required length.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the hash.</exception>
    /// <exception cref="OperationCanceledException"><paramref name="cancellationToken"/> was canceled.</exception>
    public static Task ComputeHashAsync(Memory<byte> hash, Stream input, CancellationToken cancellationToken = default) => CryptoKeyLessHash<LowLevel.Sha3512>.ComputeHashAsync(hash, input, cancellationToken);

    /// <summary>Creates an operation that accepts a message in several parts.</summary>
    /// <returns>A new incremental SHA3-512 operation.</returns>
    /// <exception cref="LibSodiumException">libsodium could not initialize the operation.</exception>
    public static ICryptoIncrementalOperation CreateIncrementalHash() => new CryptoKeyLessHashIncremental<LowLevel.Sha3512>();
}
