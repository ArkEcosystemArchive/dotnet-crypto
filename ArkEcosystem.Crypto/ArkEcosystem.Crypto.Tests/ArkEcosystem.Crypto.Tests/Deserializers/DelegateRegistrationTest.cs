// Author:
//       Brian Faust <brian@ark.io>
//
// Copyright (c) 2018 Ark Ecosystem <info@ark.io>
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin.DataEncoders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ArkEcosystem.Crypto.Tests.Deserializers
{
    [TestClass]
    public class DelegateRegistrationTest
    {
        [TestMethod]
        public void Should_Deserialize_The_Transaction()
        {
            var fixture = File.ReadAllText("../../../fixtures/delegate_registration.json");
            var transaction = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(fixture);
            var actual = new Deserializer(transaction["serialized"]).Deserialize();

            Assert.AreEqual((UInt64)transaction["amount"], actual.Amount);
            Assert.AreEqual((UInt64)transaction["fee"], actual.Fee);
            Assert.AreEqual((string)transaction["asset"]["delegate"]["username"], actual.Asset["delegate"]["username"]);
            Assert.AreEqual(transaction["id"], actual.Id);
            Assert.AreEqual(transaction["network"], actual.Network);
            Assert.AreEqual(transaction["senderPublicKey"], actual.SenderPublicKey);
            Assert.AreEqual(transaction["signature"], actual.Signature);
            Assert.AreEqual(transaction["timestamp"], actual.Timestamp);
            Assert.AreEqual(transaction["type"], actual.Type);
            Assert.AreEqual(transaction["version"], actual.Version);

            Assert.AreEqual(transaction["serialized"], Encoders.Hex.EncodeData(new Serializer(actual).Serialize()));
        }
    }
}
