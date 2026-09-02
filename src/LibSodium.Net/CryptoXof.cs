using LibSodium.LowLevel;

namespace LibSodium;

/// <summary>Provides shared one-step and incremental operations for a variable-length hash algorithm.</summary>
/// <typeparam name="T">The low-level algorithm to use.</typeparam>
internal static class CryptoXof<T> where T : IXof
{
    /// <summary>Creates variable-length output from an input message.</summary>
    /// <param name="output">A non-empty buffer that receives the output.</param>
    /// <param name="input">The message to process.</param>
    /// <exception cref="ArgumentException"><paramref name="output"/> is empty.</exception>
    /// <exception cref="LibSodiumException">libsodium could not create the output.</exception>
    public static void Compute(Span<byte> output, ReadOnlySpan<byte> input)
    {
        if (output.IsEmpty)
            throw new ArgumentException("Output must not be empty.", nameof(output));

        LibraryInitializer.EnsureInitialized();
        if (T.Compute(output, input) != 0)
            throw new LibSodiumException("Extendable-output computation failed.");
    }

    /// <summary>Creates an operation that accepts input in parts.</summary>
    /// <param name="domain">An optional custom marker byte, or <see langword="null"/> to use the standard marker.</param>
    /// <returns>A new incremental operation.</returns>
    /// <exception cref="LibSodiumException">libsodium could not initialize the operation.</exception>
    public static ICryptoXofOperation CreateIncremental(byte? domain = null) => new CryptoXofIncremental<T>(domain);
}

/// <summary>Stores the state for an incremental variable-length hash operation.</summary>
/// <typeparam name="T">The low-level algorithm to use.</typeparam>
internal sealed class CryptoXofIncremental<T> : ICryptoXofOperation where T : IXof
{
    private readonly byte[] state;
    private bool squeezing;
    private bool disposed;

    /// <summary>Creates and initializes a new operation.</summary>
    /// <param name="domain">An optional custom marker byte, or <see langword="null"/> to use the standard marker.</param>
    /// <exception cref="LibSodiumException">libsodium could not initialize the operation.</exception>
    public CryptoXofIncremental(byte? domain)
    {
        LibraryInitializer.EnsureInitialized();
        state = new byte[T.StateLen];
        int result = domain.HasValue ? T.Init(state, domain.Value) : T.Init(state);
        if (result != 0)
            throw new LibSodiumException("Failed to initialize the extendable-output operation.");
    }

    /// <inheritdoc />
    public void Update(ReadOnlySpan<byte> input)
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        if (squeezing)
            throw new InvalidOperationException("Cannot absorb input after output squeezing has started.");
        if (T.Update(state, input) != 0)
            throw new LibSodiumException("Failed to update the extendable-output operation.");
    }

    /// <inheritdoc />
    public void Squeeze(Span<byte> output)
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        if (output.IsEmpty)
            throw new ArgumentException("Output must not be empty.", nameof(output));
        if (T.Squeeze(state, output) != 0)
            throw new LibSodiumException("Failed to squeeze extendable output.");
        squeezing = true;
    }

    /// <summary>Clears the stored state and prevents further use of this operation.</summary>
    public void Dispose()
    {
        if (disposed)
            return;
        disposed = true;
        SecureMemory.MemZero(state);
    }
}
