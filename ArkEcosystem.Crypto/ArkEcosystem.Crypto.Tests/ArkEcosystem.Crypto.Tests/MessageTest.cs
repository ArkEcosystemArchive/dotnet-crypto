using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArkEcosystem.Crypto.Tests
{
    [TestClass]
    public class MessageTest
    {
        [TestMethod]
        public void Should_Sign_A_Message()
        {
            var message = Message.Sign("Hello World", "passphrase");

            Assert.IsNotNull(message);
        }

        [TestMethod]
        public void Should_Verify_A_Message()
        {
            var message = Message.Sign("Hello World", "passphrase");

            Assert.IsTrue(message.Verify());
        }
    }
}
