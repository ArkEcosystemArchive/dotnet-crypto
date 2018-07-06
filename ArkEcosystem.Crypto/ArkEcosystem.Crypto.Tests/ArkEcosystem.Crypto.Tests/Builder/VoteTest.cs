using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArkEcosystem.Crypto.Tests.Builder
{
    [TestClass]
    public class VoteTest
    {
        [TestMethod]
        public void Should_Create_Transaction_With_Secret()
        {
            var votes = new List<string>();
            votes.Add("+034151a3ec46b5670a682b0a63394f863587d1bc97483b1b6c70eb58e7f0aed192");

            var transaction = Crypto.Builder.Vote.Create(
                votes,
                "This is a top secret passphrase"
            );

            Assert.IsTrue(transaction.Verify());
        }

        [TestMethod]
        public void Should_Create_Transaction_With_Second_Secret()
        {
            var votes = new List<string>();
            votes.Add("+034151a3ec46b5670a682b0a63394f863587d1bc97483b1b6c70eb58e7f0aed192");

            var transaction = Crypto.Builder.Vote.Create(
                votes,
                "This is a top secret passphrase",
                "this is a top secret second passphrase"
            );

            Assert.IsTrue(transaction.Verify());
        }
    }
}
