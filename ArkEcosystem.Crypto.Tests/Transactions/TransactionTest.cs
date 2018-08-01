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
using System;

namespace ArkEcosystem.Crypto.Tests.Transactions
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void Serialize()
        {
            var fixture = TestHelper.ReadTransactionFixture("transfer", "passphrase");
            var transaction = new Crypto.Transactions.Deserializer(fixture["serialized"]).Deserialize();

            var serializedTransaction = transaction.Serialize();
            Assert.AreEqual(fixture["serialized"], Encoders.Hex.EncodeData(serializedTransaction));
        }
        
        [TestMethod]
        public void Deserialize()
        {
            var fixture = TestHelper.ReadTransactionFixture("transfer", "passphrase");
            var serializedTransaction = fixture["serialized"];
            
            var transaction = Crypto.Transactions.Transaction.Deserialize(serializedTransaction);
            Assert.AreEqual(0, transaction.Type);
            Assert.AreEqual(200000000ul, transaction.Amount);
            Assert.AreEqual(10000000ul, transaction.Fee);
            Assert.AreEqual("D61mfSggzbvQgTUe6JhYKH2doHaqJ3Dyib", transaction.RecipientId);
            Assert.AreEqual(41268326u, transaction.Timestamp);
            Assert.AreEqual("034151a3ec46b5670a682b0a63394f863587d1bc97483b1b6c70eb58e7f0aed192", transaction.SenderPublicKey);
            Assert.AreEqual("3044022002994b30e08b58825c8c16ebf2cc693cfe706fb26571674784ead098accc89d702205b79dedc752a84504ecfe4b9e1292997f22260ee4daa102d2d9a61432d93b286", transaction.Signature);
            Assert.AreEqual("da61c6cba363cc39baa0ca3f9ba2c5db81b9805045bd0b9fc58af07ad4206856", transaction.Id);
        }
        
        [TestMethod]
        public void ToDictionary()
        {
            var fixture = TestHelper.ReadTransactionFixture("transfer", "passphrase");
            var transaction = new Crypto.Transactions.Deserializer(fixture["serialized"]).Deserialize();
            var dictionary = transaction.ToDictionary();
            
            Assert.AreEqual(dictionary["amount"], transaction.Amount);
            Assert.AreEqual(dictionary["asset"], transaction.Asset);
            Assert.AreEqual(dictionary["fee"], transaction.Fee);
            Assert.AreEqual(dictionary["id"], transaction.Id);
            Assert.AreEqual(dictionary["network"], transaction.Network);
            Assert.AreEqual(dictionary["recipientId"], transaction.RecipientId);
            Assert.AreEqual(dictionary["secondSignature"], transaction.SecondSignature);
            Assert.AreEqual(dictionary["senderPublicKey"], transaction.SenderPublicKey);
            Assert.AreEqual(dictionary["signature"], transaction.Signature);
            Assert.AreEqual(dictionary["signatures"], transaction.Signatures);
            Assert.AreEqual(dictionary["signSignature"], transaction.SignSignature);
            Assert.AreEqual(dictionary["timestamp"], transaction.Timestamp);
            Assert.AreEqual(dictionary["type"], transaction.Type);
            Assert.AreEqual(dictionary["vendorField"], transaction.VendorField);
            Assert.AreEqual(dictionary["version"], transaction.Version);
        }
        
        [TestMethod]
        public void ToJson()
        {
            var expectedJson = "{\"amount\":200000000,\"asset\":{},\"fee\":10000000,\"id\":\"da61c6cba363cc39baa0ca3f9ba2c5db81b9805045bd0b9fc58af07ad4206856\",\"network\":30,\"recipientId\":\"D61mfSggzbvQgTUe6JhYKH2doHaqJ3Dyib\",\"secondSignature\":null,\"senderPublicKey\":\"034151a3ec46b5670a682b0a63394f863587d1bc97483b1b6c70eb58e7f0aed192\",\"signature\":\"3044022002994b30e08b58825c8c16ebf2cc693cfe706fb26571674784ead098accc89d702205b79dedc752a84504ecfe4b9e1292997f22260ee4daa102d2d9a61432d93b286\",\"signatures\":null,\"signSignature\":null,\"timestamp\":41268326,\"type\":0,\"vendorField\":null,\"version\":1}";
            
            var fixture = TestHelper.ReadTransactionFixture("transfer", "passphrase");
            var transaction = new Crypto.Transactions.Deserializer(fixture["serialized"]).Deserialize();
            var json = transaction.ToJson();
            
            Assert.AreEqual(expectedJson, json);
        }
    }
}
