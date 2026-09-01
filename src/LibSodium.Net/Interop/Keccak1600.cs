using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LibSodium.Interop;

internal static partial class Native
{
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_core_keccak1600_statebytes))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial nuint crypto_core_keccak1600_statebytes();

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_core_keccak1600_init))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void crypto_core_keccak1600_init(Span<byte> state);

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_core_keccak1600_xor_bytes))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void crypto_core_keccak1600_xor_bytes(Span<byte> state, ReadOnlySpan<byte> bytes, nuint offset, nuint length);

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_core_keccak1600_extract_bytes))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void crypto_core_keccak1600_extract_bytes(ReadOnlySpan<byte> state, Span<byte> bytes, nuint offset, nuint length);

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_core_keccak1600_permute_24))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void crypto_core_keccak1600_permute_24(Span<byte> state);

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_core_keccak1600_permute_12))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void crypto_core_keccak1600_permute_12(Span<byte> state);
}
