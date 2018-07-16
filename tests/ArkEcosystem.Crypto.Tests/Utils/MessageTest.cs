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

namespace ArkEcosystem.Crypto.Tests.Utils
{
    [TestClass]
    public class MessageTest
    {
        [TestMethod]
        public void Should_Sign_A_Message()
        {
            var fixture = TestHelper.ReadFixture("message");
            var message = Message.Sign((string)fixture["data"]["message"], fixture["passphrase"]);

            Assert.AreEqual((string)fixture["data"]["publickey"], message.publicKey);
            Assert.AreEqual((string)fixture["data"]["signature"], message.signature);
            Assert.AreEqual((string)fixture["data"]["message"], message.message);
        }

        [TestMethod]
        public void Should_Verify_A_Message()
        {
            var fixture = TestHelper.ReadFixture("message");
            var message = new Message(
                (string)fixture["data"]["publickey"],
                (string)fixture["data"]["signature"],
                (string)fixture["data"]["message"]
            );

            Assert.IsTrue(message.Verify());
        }

        [TestMethod]
        public void Should_Convert_Message_To_Json()
        {
            var fixture = TestHelper.ReadFixture("message");
            var message = new Message(
                (string)fixture["data"]["publickey"],
                (string)fixture["data"]["signature"],
                (string)fixture["data"]["message"]
            );

            Assert.AreEqual(fixture["data"].ToString(), message.ToJson());
        }
    }
}
