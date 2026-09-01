using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LibSodium.Interop;

internal static partial class Native
{
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_kem_seed_keypair))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_kem_seed_keypair(Span<byte> publicKey, Span<byte> secretKey, ReadOnlySpan<byte> seed);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_kem_keypair))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_kem_keypair(Span<byte> publicKey, Span<byte> secretKey);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_kem_enc))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_kem_enc(Span<byte> ciphertext, Span<byte> sharedSecret, ReadOnlySpan<byte> publicKey);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_kem_dec))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_kem_dec(Span<byte> sharedSecret, ReadOnlySpan<byte> ciphertext, ReadOnlySpan<byte> secretKey);

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_kem_xwing_seed_keypair))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_kem_xwing_seed_keypair(Span<byte> publicKey, Span<byte> secretKey, ReadOnlySpan<byte> seed);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_kem_xwing_keypair))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_kem_xwing_keypair(Span<byte> publicKey, Span<byte> secretKey);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_kem_xwing_enc))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_kem_xwing_enc(Span<byte> ciphertext, Span<byte> sharedSecret, ReadOnlySpan<byte> publicKey);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_kem_xwing_enc_deterministic))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_kem_xwing_enc_deterministic(Span<byte> ciphertext, Span<byte> sharedSecret, ReadOnlySpan<byte> publicKey, ReadOnlySpan<byte> seed);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_kem_xwing_dec))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_kem_xwing_dec(Span<byte> sharedSecret, ReadOnlySpan<byte> ciphertext, ReadOnlySpan<byte> secretKey);

    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_kem_mlkem768_seed_keypair))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_kem_mlkem768_seed_keypair(Span<byte> publicKey, Span<byte> secretKey, ReadOnlySpan<byte> seed);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_kem_mlkem768_keypair))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_kem_mlkem768_keypair(Span<byte> publicKey, Span<byte> secretKey);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_kem_mlkem768_enc))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_kem_mlkem768_enc(Span<byte> ciphertext, Span<byte> sharedSecret, ReadOnlySpan<byte> publicKey);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_kem_mlkem768_enc_deterministic))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_kem_mlkem768_enc_deterministic(Span<byte> ciphertext, Span<byte> sharedSecret, ReadOnlySpan<byte> publicKey, ReadOnlySpan<byte> seed);
    [LibraryImport(LibSodiumNativeLibraryName, EntryPoint = nameof(crypto_kem_mlkem768_dec))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int crypto_kem_mlkem768_dec(Span<byte> sharedSecret, ReadOnlySpan<byte> ciphertext, ReadOnlySpan<byte> secretKey);
}
