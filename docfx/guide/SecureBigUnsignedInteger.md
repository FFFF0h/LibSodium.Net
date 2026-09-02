# SecureBigUnsignedInteger

The `SecureBigUnsignedInteger` class provides constant-time operations for working with large, little-endian, unsigned integers represented as `Span<byte>`. It wraps several low-level functions from libsodium that are specifically designed to be safe against side-channel attacks.

> Based on libsodium’s [Integer manipulation helpers](https://doc.libsodium.org/helpers#incrementing-large-numbers)<br/>
> ℹ️ *See also:* [API Reference for `SecureBigUnsignedInteger`](../api/LibSodium.SecureBigUnsignedInteger.yml)

---

## Security Considerations

All operations in this class are evaluated in **constant time with respect to buffer size**, which helps prevent timing-based side-channel attacks. These methods are suitable for cryptographic contexts where comparisons, additions, or manipulations of secrets must not leak information through execution timing.

---

## Features

- Constant-time comparison, equality, addition, and subtraction.
- Increment by 1 or arbitrary 64-bit value.
- Zero-checking.
- Supports arbitrary-sized numbers (as long as both operands have the same length).
- Uses spans to avoid heap allocations.

---

## Usage Examples

### Constant-Time Equality Check

```csharp
Span<byte> a = stackalloc byte[] { 1, 2, 3 };
Span<byte> b = stackalloc byte[] { 1, 2, 3 };
bool areEqual = SecureBigUnsignedInteger.Equals(a, b); // true
```

### Constant-Time Compare

```csharp
int cmp = SecureBigUnsignedInteger.Compare(a, b); // 0 if equal, <0 if a < b, >0 if a > b
```

### Increment

```csharp
Span<byte> number = stackalloc byte[4];
SecureBigUnsignedInteger.Increment(number); // adds 1
SecureBigUnsignedInteger.Increment(number, 42); // adds 42
```

### Addition and Subtraction

```csharp
Span<byte> a = stackalloc byte[] { 5, 0, 0 };
ReadOnlySpan<byte> b = stackalloc byte[] { 3, 0, 0 };

SecureBigUnsignedInteger.Add(a, b);      // a = a + b
SecureBigUnsignedInteger.Subtract(a, b); // a = a - b
```

> Both operands must have the same length. Otherwise, `ArgumentException` is thrown.

### Zero Check

```csharp
Span<byte> n = stackalloc byte[] { 0, 0, 0 };
bool isZero = SecureBigUnsignedInteger.IsZero(n); // true
```

---

This API is ideal for low-level cryptographic arithmetic where performance, determinism, and side-channel resistance are essential.

