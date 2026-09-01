using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LibSodium.Interop;

internal static partial class Native
{
    internal const int CRYPTO_XOF_SHAKE128_BLOCKBYTES = 168;
    internal const int CRYPTO_XOF_SHAKE256_BLOCKBYTES = 136;
    internal const int CRYPTO_XOF_STATEBYTES = 256;
    internal const byte CRYPTO_XOF_DOMAIN_STANDARD = 0x1f;

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_xof_shake128))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_xof_shake128(Span<byte> output, nuint outputLength, ReadOnlySpan<byte> input, ulong inputLength);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_xof_shake128_init))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_xof_shake128_init(Span<byte> state);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_xof_shake128_init_with_domain))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_xof_shake128_init_with_domain(Span<byte> state, byte domain);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_xof_shake128_update))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_xof_shake128_update(Span<byte> state, ReadOnlySpan<byte> input, ulong inputLength);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_xof_shake128_squeeze))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_xof_shake128_squeeze(Span<byte> state, Span<byte> output, nuint outputLength);

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_xof_shake256))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_xof_shake256(Span<byte> output, nuint outputLength, ReadOnlySpan<byte> input, ulong inputLength);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_xof_shake256_init))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_xof_shake256_init(Span<byte> state);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_xof_shake256_init_with_domain))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_xof_shake256_init_with_domain(Span<byte> state, byte domain);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_xof_shake256_update))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_xof_shake256_update(Span<byte> state, ReadOnlySpan<byte> input, ulong inputLength);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_xof_shake256_squeeze))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_xof_shake256_squeeze(Span<byte> state, Span<byte> output, nuint outputLength);

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_xof_turboshake128))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_xof_turboshake128(Span<byte> output, nuint outputLength, ReadOnlySpan<byte> input, ulong inputLength);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_xof_turboshake128_init))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_xof_turboshake128_init(Span<byte> state);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_xof_turboshake128_init_with_domain))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_xof_turboshake128_init_with_domain(Span<byte> state, byte domain);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_xof_turboshake128_update))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_xof_turboshake128_update(Span<byte> state, ReadOnlySpan<byte> input, ulong inputLength);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_xof_turboshake128_squeeze))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_xof_turboshake128_squeeze(Span<byte> state, Span<byte> output, nuint outputLength);

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_xof_turboshake256))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_xof_turboshake256(Span<byte> output, nuint outputLength, ReadOnlySpan<byte> input, ulong inputLength);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_xof_turboshake256_init))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_xof_turboshake256_init(Span<byte> state);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_xof_turboshake256_init_with_domain))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_xof_turboshake256_init_with_domain(Span<byte> state, byte domain);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_xof_turboshake256_update))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_xof_turboshake256_update(Span<byte> state, ReadOnlySpan<byte> input, ulong inputLength);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_xof_turboshake256_squeeze))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_xof_turboshake256_squeeze(Span<byte> state, Span<byte> output, nuint outputLength);
}
