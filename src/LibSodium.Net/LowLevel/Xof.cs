using LibSodium.Interop;

namespace LibSodium.LowLevel;

/// <summary>Defines the low-level operations required by a variable-length hash algorithm.</summary>
internal interface IXof
{
    /// <summary>Gets the number of bytes required to store the operation state.</summary>
    /// <value>The required state length.</value>
    static abstract int StateLen { get; }
    /// <summary>Creates output from a complete input message in one call.</summary>
    /// <param name="output">The buffer that receives the output.</param>
    /// <param name="input">The complete input message.</param>
    /// <returns>Zero on success; otherwise, a nonzero error code.</returns>
    static abstract int Compute(Span<byte> output, ReadOnlySpan<byte> input);
    /// <summary>Initializes a state with the standard marker byte.</summary>
    /// <param name="state">The state buffer to initialize.</param>
    /// <returns>Zero on success; otherwise, a nonzero error code.</returns>
    static abstract int Init(Span<byte> state);
    /// <summary>Initializes a state with a custom marker byte.</summary>
    /// <param name="state">The state buffer to initialize.</param>
    /// <param name="domain">The custom marker byte.</param>
    /// <returns>Zero on success; otherwise, a nonzero error code.</returns>
    static abstract int Init(Span<byte> state, byte domain);
    /// <summary>Adds input bytes to an initialized state.</summary>
    /// <param name="state">The current state buffer.</param>
    /// <param name="input">The input bytes to add.</param>
    /// <returns>Zero on success; otherwise, a nonzero error code.</returns>
    static abstract int Update(Span<byte> state, ReadOnlySpan<byte> input);
    /// <summary>Writes the next output bytes from a state.</summary>
    /// <param name="state">The current state buffer.</param>
    /// <param name="output">The buffer that receives the next output bytes.</param>
    /// <returns>Zero on success; otherwise, a nonzero error code.</returns>
    static abstract int Squeeze(Span<byte> state, Span<byte> output);
}

/// <summary>Connects SHAKE128 native functions to the shared variable-length hash wrapper.</summary>
internal readonly struct Shake128 : IXof
{
    /// <inheritdoc />
    public static int StateLen => Native.CRYPTO_XOF_STATEBYTES;
    /// <inheritdoc />
    public static int Compute(Span<byte> output, ReadOnlySpan<byte> input) => Native.crypto_xof_shake128(output, (nuint)output.Length, input, (ulong)input.Length);
    /// <inheritdoc />
    public static int Init(Span<byte> state) => Native.crypto_xof_shake128_init(state);
    /// <inheritdoc />
    public static int Init(Span<byte> state, byte domain) => Native.crypto_xof_shake128_init_with_domain(state, domain);
    /// <inheritdoc />
    public static int Update(Span<byte> state, ReadOnlySpan<byte> input) => Native.crypto_xof_shake128_update(state, input, (ulong)input.Length);
    /// <inheritdoc />
    public static int Squeeze(Span<byte> state, Span<byte> output) => Native.crypto_xof_shake128_squeeze(state, output, (nuint)output.Length);
}

/// <summary>Connects SHAKE256 native functions to the shared variable-length hash wrapper.</summary>
internal readonly struct Shake256 : IXof
{
    /// <inheritdoc />
    public static int StateLen => Native.CRYPTO_XOF_STATEBYTES;
    /// <inheritdoc />
    public static int Compute(Span<byte> output, ReadOnlySpan<byte> input) => Native.crypto_xof_shake256(output, (nuint)output.Length, input, (ulong)input.Length);
    /// <inheritdoc />
    public static int Init(Span<byte> state) => Native.crypto_xof_shake256_init(state);
    /// <inheritdoc />
    public static int Init(Span<byte> state, byte domain) => Native.crypto_xof_shake256_init_with_domain(state, domain);
    /// <inheritdoc />
    public static int Update(Span<byte> state, ReadOnlySpan<byte> input) => Native.crypto_xof_shake256_update(state, input, (ulong)input.Length);
    /// <inheritdoc />
    public static int Squeeze(Span<byte> state, Span<byte> output) => Native.crypto_xof_shake256_squeeze(state, output, (nuint)output.Length);
}

/// <summary>Connects TurboSHAKE128 native functions to the shared variable-length hash wrapper.</summary>
internal readonly struct TurboShake128 : IXof
{
    /// <inheritdoc />
    public static int StateLen => Native.CRYPTO_XOF_STATEBYTES;
    /// <inheritdoc />
    public static int Compute(Span<byte> output, ReadOnlySpan<byte> input) => Native.crypto_xof_turboshake128(output, (nuint)output.Length, input, (ulong)input.Length);
    /// <inheritdoc />
    public static int Init(Span<byte> state) => Native.crypto_xof_turboshake128_init(state);
    /// <inheritdoc />
    public static int Init(Span<byte> state, byte domain) => Native.crypto_xof_turboshake128_init_with_domain(state, domain);
    /// <inheritdoc />
    public static int Update(Span<byte> state, ReadOnlySpan<byte> input) => Native.crypto_xof_turboshake128_update(state, input, (ulong)input.Length);
    /// <inheritdoc />
    public static int Squeeze(Span<byte> state, Span<byte> output) => Native.crypto_xof_turboshake128_squeeze(state, output, (nuint)output.Length);
}

/// <summary>Connects TurboSHAKE256 native functions to the shared variable-length hash wrapper.</summary>
internal readonly struct TurboShake256 : IXof
{
    /// <inheritdoc />
    public static int StateLen => Native.CRYPTO_XOF_STATEBYTES;
    /// <inheritdoc />
    public static int Compute(Span<byte> output, ReadOnlySpan<byte> input) => Native.crypto_xof_turboshake256(output, (nuint)output.Length, input, (ulong)input.Length);
    /// <inheritdoc />
    public static int Init(Span<byte> state) => Native.crypto_xof_turboshake256_init(state);
    /// <inheritdoc />
    public static int Init(Span<byte> state, byte domain) => Native.crypto_xof_turboshake256_init_with_domain(state, domain);
    /// <inheritdoc />
    public static int Update(Span<byte> state, ReadOnlySpan<byte> input) => Native.crypto_xof_turboshake256_update(state, input, (ulong)input.Length);
    /// <inheritdoc />
    public static int Squeeze(Span<byte> state, Span<byte> output) => Native.crypto_xof_turboshake256_squeeze(state, output, (nuint)output.Length);
}
