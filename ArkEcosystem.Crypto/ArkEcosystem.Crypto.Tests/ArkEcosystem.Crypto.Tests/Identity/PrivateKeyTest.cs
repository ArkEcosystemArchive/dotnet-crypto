using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArkEcosystem.Crypto.Tests.Identity
{
    [TestClass]
    public class PrivateKeyTest
    {
        [Ignore]
        [TestMethod]
        public void Should_Get_The_Address_From_Public_Key()
        {
            var actual = Crypto.Identity.PrivateKey.FromSecret("this is a top secret passphrase");

            Assert.AreEqual("d8839c2432bfd0a67ef10a804ba991eabba19f154a3d707917681d45822a5712", actual);
        }
    }
}
