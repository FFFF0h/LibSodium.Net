using LibSodium.Tests;

namespace LibSodium.Net.Tests;

public class CryptoKemTests
{
    [Test]
    public void DefaultXWing_RoundTrip_DerivesSameSharedSecret()
    {
        byte[] publicKey = new byte[CryptoKem.PublicKeyLen];
        byte[] secretKey = new byte[CryptoKem.SecretKeyLen];
        byte[] ciphertext = new byte[CryptoKem.CiphertextLen];
        byte[] encapsulated = new byte[CryptoKem.SharedSecretLen];
        byte[] decapsulated = new byte[CryptoKem.SharedSecretLen];
        CryptoKem.GenerateKeyPair(publicKey, secretKey);
        CryptoKem.Encapsulate(ciphertext, encapsulated, publicKey);
        CryptoKem.Decapsulate(decapsulated, ciphertext, secretKey);
        decapsulated.ShouldBe(encapsulated);
    }

    [Test]
    public void XWing_DeterministicOperations_AreReproducible()
    {
        byte[] keySeed = Enumerable.Range(0, CryptoKemXWing.SeedLen).Select(i => (byte)i).ToArray();
        byte[] encapsulationSeed = Enumerable.Range(0, CryptoKemXWing.EncapsulationSeedLen).Select(i => (byte)(255 - i)).ToArray();
        byte[] publicKey1 = new byte[CryptoKemXWing.PublicKeyLen];
        byte[] secretKey1 = new byte[CryptoKemXWing.SecretKeyLen];
        byte[] publicKey2 = new byte[CryptoKemXWing.PublicKeyLen];
        byte[] secretKey2 = new byte[CryptoKemXWing.SecretKeyLen];
        CryptoKemXWing.GenerateKeyPairDeterministically(publicKey1, secretKey1, keySeed);
        CryptoKemXWing.GenerateKeyPairDeterministically(publicKey2, secretKey2, keySeed);
        publicKey2.ShouldBe(publicKey1);
        secretKey2.ShouldBe(secretKey1);

        byte[] ciphertext1 = new byte[CryptoKemXWing.CiphertextLen];
        byte[] sharedSecret1 = new byte[CryptoKemXWing.SharedSecretLen];
        byte[] ciphertext2 = new byte[CryptoKemXWing.CiphertextLen];
        byte[] sharedSecret2 = new byte[CryptoKemXWing.SharedSecretLen];
        CryptoKemXWing.EncapsulateDeterministically(ciphertext1, sharedSecret1, publicKey1, encapsulationSeed);
        CryptoKemXWing.EncapsulateDeterministically(ciphertext2, sharedSecret2, publicKey1, encapsulationSeed);
        ciphertext2.ShouldBe(ciphertext1);
        sharedSecret2.ShouldBe(sharedSecret1);
    }

    [Test]
    public void MlKem768_RoundTrip_DerivesSameSharedSecret()
    {
        byte[] publicKey = new byte[CryptoKemMlKem768.PublicKeyLen];
        byte[] secretKey = new byte[CryptoKemMlKem768.SecretKeyLen];
        byte[] ciphertext = new byte[CryptoKemMlKem768.CiphertextLen];
        byte[] encapsulated = new byte[CryptoKemMlKem768.SharedSecretLen];
        byte[] decapsulated = new byte[CryptoKemMlKem768.SharedSecretLen];
        CryptoKemMlKem768.GenerateKeyPair(publicKey, secretKey);
        CryptoKemMlKem768.Encapsulate(ciphertext, encapsulated, publicKey);
        CryptoKemMlKem768.Decapsulate(decapsulated, ciphertext, secretKey);
        decapsulated.ShouldBe(encapsulated);
    }

    [Test]
    public void MlKem768_InvalidPublicKeyLength_Throws()
    {
        AssertLite.Throws<ArgumentException>(() =>
        {
            byte[] ciphertext = new byte[CryptoKemMlKem768.CiphertextLen];
            byte[] sharedSecret = new byte[CryptoKemMlKem768.SharedSecretLen];
            CryptoKemMlKem768.Encapsulate(ciphertext, sharedSecret, new byte[CryptoKemMlKem768.PublicKeyLen - 1]);
        });
    }
}
