using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArkEcosystem.Crypto.Tests.Identity
{
    [TestClass]
    public class WIFTest
    {
        [TestMethod]
        public void Should_Get_The_Address_From_Public_Key()
        {
            var actual = Crypto.Identity.WIF.FromSecret("this is a top secret passphrase");

            Assert.AreEqual("SGq4xLgZKCGxs7bjmwnBrWcT4C1ADFEermj846KC97FSv1WFD1dA", actual);
        }
    }
}
