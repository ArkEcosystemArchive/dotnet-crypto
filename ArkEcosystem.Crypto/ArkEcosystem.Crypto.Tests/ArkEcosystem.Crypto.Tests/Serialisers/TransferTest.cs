using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin.DataEncoders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ArkEcosystem.Crypto.Tests.Serialisers
{
    [TestClass]
    public class TransferTest
    {
        [TestMethod]
        public void Should_Serialise_The_Transaction()
        {
            var fixture = File.ReadAllText("../../../fixtures/vote.json");
            var transaction = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(fixture);
            var actual = new Deserialiser(transaction["serialized"]).Deserialise();

            Assert.AreEqual(transaction["serialized"], Encoders.Hex.EncodeData(new Serialiser(actual).Serialise()));
        }
    }
}
