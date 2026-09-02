using System.Text;
using LibSodium.Tests;

namespace LibSodium.Net.Tests;

public class CryptoSha3AndXofTests
{
    [Test]
    public void Sha3256_Empty_MatchesNistVector()
    {
        Span<byte> hash = stackalloc byte[CryptoSha3256.HashLen];
        CryptoSha3256.ComputeHash(hash, []);
        hash.ShouldBe(Convert.FromHexString("A7FFC6F8BF1ED76651C14756A061D662F580FF4DE43B49FA82D80A4B80F8434A"));
    }

    [Test]
    public void Sha3512_Empty_MatchesNistVector()
    {
        Span<byte> hash = stackalloc byte[CryptoSha3512.HashLen];
        CryptoSha3512.ComputeHash(hash, []);
        hash.ShouldBe(Convert.FromHexString("A69F73CCA23A9AC5C8B567DC185A756E97C982164FE25859E0D1DCC1475C80A615B2123AF1F5F94C11E3E9402C3AC558F500199D95B6D3E301758586281DCD26"));
    }

    [Test]
    public void Sha3_Incremental_MatchesOneShot()
    {
        byte[] message = Encoding.UTF8.GetBytes("libsodium 1.0.22");
        Span<byte> expected = stackalloc byte[CryptoSha3256.HashLen];
        Span<byte> actual = stackalloc byte[CryptoSha3256.HashLen];
        CryptoSha3256.ComputeHash(expected, message);
        using var incremental = CryptoSha3256.CreateIncrementalHash();
        incremental.Update(message.AsSpan(0, 9));
        incremental.Update(message.AsSpan(9));
        incremental.Final(actual);
        actual.ShouldBe(expected);
    }

    [Test]
    public void Shake128_Empty_MatchesNistVector()
    {
        Span<byte> output = stackalloc byte[32];
        CryptoShake128.Compute(output, []);
        output.ShouldBe(Convert.FromHexString("7F9C2BA4E88F827D616045507605853E D73B8093F6EFBC88EB1A6EACFA66EF26".Replace(" ", "")));
    }

    [Test]
    public void Shake256_Empty_MatchesNistVector()
    {
        Span<byte> output = stackalloc byte[64];
        CryptoShake256.Compute(output, []);
        output.ShouldBe(Convert.FromHexString("46B9DD2B0BA88D13233B3FEB743EEB24 3FCD52EA62B81B82B50C27646ED5762F D75DC4DDD8C0F200CB05019D67B592F6 FC821C49479AB48640292EACB3B7C4BE".Replace(" ", "")));
    }

    [Test]
    public void Shake_IncrementalSqueezing_MatchesOneShot()
    {
        byte[] message = Encoding.UTF8.GetBytes("incremental XOF");
        byte[] expected = new byte[91];
        byte[] actual = new byte[91];
        CryptoShake256.Compute(expected, message);
        using var incremental = CryptoShake256.CreateIncremental();
        incremental.Update(message.AsSpan(0, 5));
        incremental.Update(message.AsSpan(5));
        incremental.Squeeze(actual.AsSpan(0, 17));
        incremental.Squeeze(actual.AsSpan(17));
        actual.ShouldBe(expected);
    }

    [Test]
    public void TurboShake_Incremental_MatchesOneShot()
    {
        byte[] message = Encoding.UTF8.GetBytes("TurboSHAKE");
        byte[] expected128 = new byte[64];
        byte[] actual128 = new byte[64];
        byte[] expected256 = new byte[64];
        byte[] actual256 = new byte[64];
        CryptoTurboShake128.Compute(expected128, message);
        CryptoTurboShake256.Compute(expected256, message);
        using var incremental128 = CryptoTurboShake128.CreateIncremental();
        using var incremental256 = CryptoTurboShake256.CreateIncremental();
        incremental128.Update(message);
        incremental256.Update(message);
        incremental128.Squeeze(actual128);
        incremental256.Squeeze(actual256);
        actual128.ShouldBe(expected128);
        actual256.ShouldBe(expected256);
    }

    [Test]
    public void Keccak1600_AbsorbExtractAndPermute_Works()
    {
        using var keccak = new CryptoKeccak1600();
        byte[] input = Enumerable.Range(0, 64).Select(i => (byte)i).ToArray();
        Span<byte> extracted = stackalloc byte[input.Length];
        keccak.XorBytes(input, 32);
        keccak.ExtractBytes(extracted, 32);
        extracted.ShouldBe(input);
        keccak.Permute24();
        keccak.ExtractBytes(extracted, 32);
        extracted.SequenceEqual(input).ShouldBeFalse();
    }
}
