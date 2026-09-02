using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LibSodium.Interop;

internal static partial class Native
{
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_ipcrypt_keygen))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void crypto_ipcrypt_keygen(Span<byte> key);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_ipcrypt_nd_keygen))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void crypto_ipcrypt_nd_keygen(Span<byte> key);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_ipcrypt_ndx_keygen))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void crypto_ipcrypt_ndx_keygen(Span<byte> key);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_ipcrypt_pfx_keygen))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void crypto_ipcrypt_pfx_keygen(Span<byte> key);

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_ipcrypt_encrypt))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void crypto_ipcrypt_encrypt(Span<byte> output, ReadOnlySpan<byte> input, ReadOnlySpan<byte> key);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_ipcrypt_decrypt))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void crypto_ipcrypt_decrypt(Span<byte> output, ReadOnlySpan<byte> input, ReadOnlySpan<byte> key);

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_ipcrypt_nd_encrypt))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void crypto_ipcrypt_nd_encrypt(Span<byte> output, ReadOnlySpan<byte> input, ReadOnlySpan<byte> tweak, ReadOnlySpan<byte> key);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_ipcrypt_nd_decrypt))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void crypto_ipcrypt_nd_decrypt(Span<byte> output, ReadOnlySpan<byte> input, ReadOnlySpan<byte> key);

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_ipcrypt_ndx_encrypt))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void crypto_ipcrypt_ndx_encrypt(Span<byte> output, ReadOnlySpan<byte> input, ReadOnlySpan<byte> tweak, ReadOnlySpan<byte> key);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_ipcrypt_ndx_decrypt))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void crypto_ipcrypt_ndx_decrypt(Span<byte> output, ReadOnlySpan<byte> input, ReadOnlySpan<byte> key);

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_ipcrypt_pfx_encrypt))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void crypto_ipcrypt_pfx_encrypt(Span<byte> output, ReadOnlySpan<byte> input, ReadOnlySpan<byte> key);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_ipcrypt_pfx_decrypt))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void crypto_ipcrypt_pfx_decrypt(Span<byte> output, ReadOnlySpan<byte> input, ReadOnlySpan<byte> key);
}
