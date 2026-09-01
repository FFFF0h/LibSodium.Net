using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LibSodium.Interop;

internal static partial class Native
{
    internal const int CRYPTO_HASH_SHA3256_BYTES = 32;
    internal const int CRYPTO_HASH_SHA3512_BYTES = 64;

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_hash_sha3256_statebytes))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial nuint crypto_hash_sha3256_statebytes();

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_hash_sha3256))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_hash_sha3256(Span<byte> output, ReadOnlySpan<byte> input, ulong inputLength);

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_hash_sha3256_init))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_hash_sha3256_init(Span<byte> state);

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_hash_sha3256_update))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_hash_sha3256_update(Span<byte> state, ReadOnlySpan<byte> input, ulong inputLength);

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_hash_sha3256_final))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_hash_sha3256_final(Span<byte> state, Span<byte> output);

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_hash_sha3512_statebytes))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial nuint crypto_hash_sha3512_statebytes();

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_hash_sha3512))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_hash_sha3512(Span<byte> output, ReadOnlySpan<byte> input, ulong inputLength);

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_hash_sha3512_init))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_hash_sha3512_init(Span<byte> state);

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_hash_sha3512_update))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_hash_sha3512_update(Span<byte> state, ReadOnlySpan<byte> input, ulong inputLength);

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_hash_sha3512_final))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_hash_sha3512_final(Span<byte> state, Span<byte> output);
}
