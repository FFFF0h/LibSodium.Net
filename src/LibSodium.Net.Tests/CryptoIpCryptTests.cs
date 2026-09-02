using LibSodium.Tests;

namespace LibSodium.Net.Tests;

public class CryptoIpCryptTests
{
    private static readonly byte[] Address = Convert.FromHexString("20010DB8000000000000000000000001");

    [Test]
    public void Deterministic_RoundTrip_Works()
    {
        Span<byte> key = stackalloc byte[CryptoIpCrypt.KeyLen];
        Span<byte> ciphertext = stackalloc byte[CryptoIpCrypt.OutputLen];
        Span<byte> plaintext = stackalloc byte[CryptoIpCrypt.InputLen];
        CryptoIpCrypt.GenerateKey(key);
        CryptoIpCrypt.Encrypt(ciphertext, Address, key);
        CryptoIpCrypt.Decrypt(plaintext, ciphertext, key);
        plaintext.ShouldBe(Address);
    }

    [Test]
    public void Nd_RoundTrip_Works()
    {
        Span<byte> key = stackalloc byte[CryptoIpCrypt.NdKeyLen];
        Span<byte> tweak = stackalloc byte[CryptoIpCrypt.NdTweakLen];
        Span<byte> ciphertext = stackalloc byte[CryptoIpCrypt.NdOutputLen];
        Span<byte> plaintext = stackalloc byte[CryptoIpCrypt.InputLen];
        CryptoIpCrypt.GenerateNdKey(key);
        RandomGenerator.Fill(tweak);
        CryptoIpCrypt.EncryptNd(ciphertext, Address, tweak, key);
        CryptoIpCrypt.DecryptNd(plaintext, ciphertext, key);
        plaintext.ShouldBe(Address);
    }

    [Test]
    public void Ndx_RoundTrip_Works()
    {
        Span<byte> key = stackalloc byte[CryptoIpCrypt.NdxKeyLen];
        Span<byte> tweak = stackalloc byte[CryptoIpCrypt.NdxTweakLen];
        Span<byte> ciphertext = stackalloc byte[CryptoIpCrypt.NdxOutputLen];
        Span<byte> plaintext = stackalloc byte[CryptoIpCrypt.InputLen];
        CryptoIpCrypt.GenerateNdxKey(key);
        RandomGenerator.Fill(tweak);
        CryptoIpCrypt.EncryptNdx(ciphertext, Address, tweak, key);
        CryptoIpCrypt.DecryptNdx(plaintext, ciphertext, key);
        plaintext.ShouldBe(Address);
    }

    [Test]
    public void PrefixPreserving_RoundTrip_Works()
    {
        Span<byte> key = stackalloc byte[CryptoIpCrypt.PrefixKeyLen];
        Span<byte> ciphertext = stackalloc byte[CryptoIpCrypt.OutputLen];
        Span<byte> plaintext = stackalloc byte[CryptoIpCrypt.InputLen];
        CryptoIpCrypt.GeneratePrefixKey(key);
        CryptoIpCrypt.EncryptPrefix(ciphertext, Address, key);
        CryptoIpCrypt.DecryptPrefix(plaintext, ciphertext, key);
        plaintext.ShouldBe(Address);
    }

    [Test]
    public void InvalidTweakLength_Throws()
    {
        AssertLite.Throws<ArgumentException>(() =>
        {
            Span<byte> output = stackalloc byte[CryptoIpCrypt.NdOutputLen];
            Span<byte> key = stackalloc byte[CryptoIpCrypt.NdKeyLen];
            CryptoIpCrypt.EncryptNd(output, Address, new byte[CryptoIpCrypt.NdTweakLen - 1], key);
        });
    }
}
