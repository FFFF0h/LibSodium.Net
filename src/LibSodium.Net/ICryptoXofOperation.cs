namespace LibSodium;

/// <summary>
/// Represents a variable-length hash operation that accepts input in parts and can produce as many output bytes as needed.
/// </summary>
/// <remarks>
/// Call <see cref="Update"/> one or more times before the first call to <see cref="Squeeze"/>.
/// After output has started, more input cannot be added. Dispose the operation when it is no longer needed.
/// </remarks>
public interface ICryptoXofOperation : IDisposable
{
    /// <summary>Adds the next part of the input.</summary>
    /// <param name="input">The input bytes to add.</param>
    /// <exception cref="ObjectDisposedException">The operation has already been disposed.</exception>
    /// <exception cref="InvalidOperationException">Output has already been requested with <see cref="Squeeze"/>.</exception>
    /// <exception cref="LibSodiumException">libsodium could not process the input.</exception>
    void Update(ReadOnlySpan<byte> input);

    /// <summary>Writes the next requested bytes of output.</summary>
    /// <param name="output">A non-empty buffer that receives the next output bytes.</param>
    /// <exception cref="ArgumentException"><paramref name="output"/> is empty.</exception>
    /// <exception cref="ObjectDisposedException">The operation has already been disposed.</exception>
    /// <exception cref="LibSodiumException">libsodium could not produce the output.</exception>
    void Squeeze(Span<byte> output);
}
