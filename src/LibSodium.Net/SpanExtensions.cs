using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LibSodium;

internal static class SpanExtensions
{
    public static bool IsDefault<T>(this Span<T> span) =>
        Unsafe.IsNullRef(ref MemoryMarshal.GetReference(span));

    public static bool IsDefault<T>(this ReadOnlySpan<T> span) =>
        Unsafe.IsNullRef(ref MemoryMarshal.GetReference(span));
}
