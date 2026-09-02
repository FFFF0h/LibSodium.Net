# Design Philosophy

**LibSodium.Net** is built around a simple but powerful idea:  
> _Expose all of libsodium’s capabilities exactly as they are, without hiding anything, while making them ergonomic for .NET developers._

## Transparent
We don’t reinvent cryptographic primitives. We expose them as they are implemented in libsodium, preserving their guarantees, formats, and conventions.

## Ergonomic
Our API is intuitive and natural in .NET:
- Full support for `Span<byte>` and `ReadOnlySpan<byte>`
- Combined and detached modes
- Optional automatic nonce generation
- Additional authenticated data (AAD)
- Clear exceptions and strict input validation

## No unnecessary abstractions
We don’t force you to wrap keys into opaque classes like `Key`.  
You decide how to handle secrets: `stackalloc`, `byte[]`, derived keys, externally injected material, etc.

If you need a higher-level abstraction, you’re free to build one **on top of our minimal, consistent API**.

## Unopinionated by design
**LibSodium.Net is not opinionated.** It doesn’t tell you how to structure your cryptography — it simply gives you the tools to do it right.

## Built for

- Developers who want to **understand and control** what’s happening under the hood.
- Projects that need to **interoperate** with other libsodium implementations (in C, Rust, Go, etc.).
- Systems with flexible key management (e.g. key derivation, ephemeral keys, external sources).
- Teams who value an API that’s **simple, secure, and predictable**.
