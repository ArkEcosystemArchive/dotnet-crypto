using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArkEcosystem.Crypto.Tests.Identity
{
    [TestClass]
    public class PublicKeyTest
    {
        [TestMethod]
        public void Should_Get_The_Address_From_Public_Key()
        {
            var actual = Crypto.Identity.PublicKey.FromSecret("this is a top secret passphrase");

            Assert.AreEqual("034151a3ec46b5670a682b0a63394f863587d1bc97483b1b6c70eb58e7f0aed192", actual.ToHex());
        }
    }
}
