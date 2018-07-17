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

namespace ArkEcosystem.Crypto.Tests.Transactions.Deserializers
{
    [TestClass]
    public class DelegateRegistrationTest
    {
        [TestMethod]
        public void Should_Deserialize_The_Transaction_With_A_Passphrase()
        {
            var fixture = TestHelper.ReadTransactionFixture("delegate_registration", "passphrase");
            var transaction = fixture["data"];
            var actual = new Crypto.Transactions.Deserializer(fixture["serialized"]).Deserialize();

            Assert.AreEqual(1, actual.Version);
            Assert.AreEqual(30, actual.Network);
            Assert.AreEqual((byte)transaction["type"], actual.Type);
            Assert.AreEqual((UInt32)transaction["timestamp"], actual.Timestamp);
            Assert.AreEqual((string)transaction["senderPublicKey"], actual.SenderPublicKey);
            Assert.AreEqual((UInt64)transaction["fee"], actual.Fee);
            Assert.AreEqual((UInt64)transaction["amount"], actual.Amount);
            Assert.AreEqual((string)transaction["signature"], actual.Signature);
            Assert.AreEqual((string)transaction["id"], actual.Id);
            Assert.AreEqual((string)transaction["asset"]["delegate"]["username"], actual.Asset["delegate"]["username"]);
        }

        [TestMethod]
        public void Should_Deserialize_The_Transaction_With_A_Second_Passphrase()
        {
            var fixture = TestHelper.ReadTransactionFixture("delegate_registration", "second-passphrase");
            var transaction = fixture["data"];
            var actual = new Crypto.Transactions.Deserializer(fixture["serialized"]).Deserialize();

            Assert.AreEqual(1, actual.Version);
            Assert.AreEqual(30, actual.Network);
            Assert.AreEqual((byte)transaction["type"], actual.Type);
            Assert.AreEqual((UInt32)transaction["timestamp"], actual.Timestamp);
            Assert.AreEqual((string)transaction["senderPublicKey"], actual.SenderPublicKey);
            Assert.AreEqual((UInt64)transaction["fee"], actual.Fee);
            Assert.AreEqual((UInt64)transaction["amount"], actual.Amount);
            Assert.AreEqual((string)transaction["signature"], actual.Signature);
            Assert.AreEqual((string)transaction["signSignature"], actual.SignSignature);
            Assert.AreEqual((string)transaction["id"], actual.Id);
            Assert.AreEqual((string)transaction["asset"]["delegate"]["username"], actual.Asset["delegate"]["username"]);
        }
    }
}
