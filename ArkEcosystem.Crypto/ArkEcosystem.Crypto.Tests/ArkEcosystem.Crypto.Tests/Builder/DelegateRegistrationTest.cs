using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArkEcosystem.Crypto.Tests.Builder
{
    [TestClass]
    public class DelegateRegistrationTest
    {
        [TestMethod]
        public void Should_Create_Transaction_With_Secret()
        {
            var transaction = Crypto.Builder.DelegateRegistration.Create(
                "polopolo",
                "This is a top secret passphrase"
            );

            Assert.IsTrue(transaction.Verify());
        }

        [TestMethod]
        public void Should_Create_Transaction_With_Second_Secret()
        {
            var transaction = Crypto.Builder.DelegateRegistration.Create(
                "polopolo",
                "This is a top secret passphrase",
                "this is a top secret second passphrase"
            );

            Assert.IsTrue(transaction.Verify());
        }
    }
}
