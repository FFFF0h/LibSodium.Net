using LibSodium.Interop;

namespace LibSodium.LowLevel;

/// <summary>Connects SHA3-256 native functions to the shared hash wrapper.</summary>
internal readonly struct Sha3256 : IKeyLessHash
{
    /// <inheritdoc />
    public static int HashLen => Native.CRYPTO_HASH_SHA3256_BYTES;
    /// <inheritdoc />
    public static int StateLen => (int)Native.crypto_hash_sha3256_statebytes();
    /// <inheritdoc />
    public static int ComputeHash(Span<byte> hash, ReadOnlySpan<byte> message) => Native.crypto_hash_sha3256(hash, message, (ulong)message.Length);
    /// <inheritdoc />
    public static int Init(Span<byte> state) => Native.crypto_hash_sha3256_init(state);
    /// <inheritdoc />
    public static int Update(Span<byte> state, ReadOnlySpan<byte> message) => Native.crypto_hash_sha3256_update(state, message, (ulong)message.Length);
    /// <inheritdoc />
    public static int Final(Span<byte> state, Span<byte> hash) => Native.crypto_hash_sha3256_final(state, hash);
}

/// <summary>Connects SHA3-512 native functions to the shared hash wrapper.</summary>
internal readonly struct Sha3512 : IKeyLessHash
{
    /// <inheritdoc />
    public static int HashLen => Native.CRYPTO_HASH_SHA3512_BYTES;
    /// <inheritdoc />
    public static int StateLen => (int)Native.crypto_hash_sha3512_statebytes();
    /// <inheritdoc />
    public static int ComputeHash(Span<byte> hash, ReadOnlySpan<byte> message) => Native.crypto_hash_sha3512(hash, message, (ulong)message.Length);
    /// <inheritdoc />
    public static int Init(Span<byte> state) => Native.crypto_hash_sha3512_init(state);
    /// <inheritdoc />
    public static int Update(Span<byte> state, ReadOnlySpan<byte> message) => Native.crypto_hash_sha3512_update(state, message, (ulong)message.Length);
    /// <inheritdoc />
    public static int Final(Span<byte> state, Span<byte> hash) => Native.crypto_hash_sha3512_final(state, hash);
}
