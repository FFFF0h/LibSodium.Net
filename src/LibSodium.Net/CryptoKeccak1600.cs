using LibSodium.Interop;

namespace LibSodium;

/// <summary>
/// Holds a Keccak-1600 state and provides methods for changing, reading, and permuting it.
/// </summary>
/// <remarks>
/// This is a low-level building block for hash functions. Most applications should use a ready-made
/// hash class such as <see cref="CryptoSha3256"/> or <see cref="CryptoShake128"/> instead.
/// </remarks>
public sealed class CryptoKeccak1600 : IDisposable
{
    /// <summary>Length of the usable Keccak state in bytes.</summary>
    public const int Width = 200;

    private readonly byte[] state;
    private bool disposed;

    /// <summary>Creates a Keccak-1600 state whose bytes are all set to zero.</summary>
    public CryptoKeccak1600()
    {
        LibraryInitializer.EnsureInitialized();
        int stateBytes = (int)Native.crypto_core_keccak1600_statebytes();
        state = new byte[stateBytes];
        Native.crypto_core_keccak1600_init(state);
    }

    /// <summary>Combines bytes with part of the current state using XOR.</summary>
    /// <param name="bytes">The bytes to combine with the state.</param>
    /// <param name="offset">The zero-based position in the state where the operation starts.</param>
    /// <exception cref="ObjectDisposedException">This instance has already been disposed.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> and the input length do not fit within the state.</exception>
    public void XorBytes(ReadOnlySpan<byte> bytes, int offset = 0)
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        ValidateRange(offset, bytes.Length);
        Native.crypto_core_keccak1600_xor_bytes(state, bytes, (nuint)offset, (nuint)bytes.Length);
    }

    /// <summary>Copies bytes from part of the current state.</summary>
    /// <param name="bytes">The buffer that receives the state bytes.</param>
    /// <param name="offset">The zero-based position in the state where reading starts.</param>
    /// <exception cref="ObjectDisposedException">This instance has already been disposed.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> and the output length do not fit within the state.</exception>
    public void ExtractBytes(Span<byte> bytes, int offset = 0)
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        ValidateRange(offset, bytes.Length);
        Native.crypto_core_keccak1600_extract_bytes(state, bytes, (nuint)offset, (nuint)bytes.Length);
    }

    /// <summary>Changes the state by applying all 24 Keccak-f[1600] rounds.</summary>
    /// <exception cref="ObjectDisposedException">This instance has already been disposed.</exception>
    public void Permute24()
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        Native.crypto_core_keccak1600_permute_24(state);
    }

    /// <summary>Changes the state by applying the last 12 Keccak-f[1600] rounds.</summary>
    /// <exception cref="ObjectDisposedException">This instance has already been disposed.</exception>
    public void Permute12()
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        Native.crypto_core_keccak1600_permute_12(state);
    }

    /// <summary>Clears the state from memory and prevents further use of this instance.</summary>
    public void Dispose()
    {
        if (disposed)
            return;
        disposed = true;
        SecureMemory.MemZero(state);
    }

    private static void ValidateRange(int offset, int length)
    {
        if (offset < 0 || offset > Width - length)
            throw new ArgumentOutOfRangeException(nameof(offset));
    }
}
