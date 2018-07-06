using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArkEcosystem.Crypto.Tests.Identity
{
    [TestClass]
    public class AddressTest
    {
        [TestMethod]
        public void Should_Get_The_Address_From_Public_Key()
        {
            var publicKey = Crypto.Identity.PublicKey.FromString("034151a3ec46b5670a682b0a63394f863587d1bc97483b1b6c70eb58e7f0aed192");
            var actual = Crypto.Identity.Address.FromPublicKey(publicKey, 0x1e);

            Assert.AreEqual("D61mfSggzbvQgTUe6JhYKH2doHaqJ3Dyib", actual);
        }

        [TestMethod]
        public void Should_Get_The_Address_From_Secret()
        {
            var actual = Crypto.Identity.Address.FromSecret("this is a top secret passphrase", 0x1e);

            Assert.AreEqual("D61mfSggzbvQgTUe6JhYKH2doHaqJ3Dyib", actual);
        }

        [TestMethod]
        public void Should_Get_The_Address_From_Private_Key()
        {
            var privateKey = Crypto.Identity.PrivateKey.FromSecret("this is a top secret passphrase");
            var actual = Crypto.Identity.Address.FromPrivateKey(privateKey, 0x1e);

            Assert.AreEqual("D61mfSggzbvQgTUe6JhYKH2doHaqJ3Dyib", actual);
        }

        [TestMethod]
        public void Should_Validate_The_Address()
        {
            var actual = Crypto.Identity.Address.Validate("D61mfSggzbvQgTUe6JhYKH2doHaqJ3Dyib", 0x1e);

            Assert.IsTrue(actual);
        }
    }
}
