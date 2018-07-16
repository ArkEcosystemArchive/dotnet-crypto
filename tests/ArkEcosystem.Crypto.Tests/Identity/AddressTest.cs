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

namespace ArkEcosystem.Crypto.Tests.Identity
{
    [TestClass]
    public class AddressTest
    {
        [TestMethod]
        public void Should_Get_The_Address_From_Public_Key()
        {
            var fixture = TestHelper.ReadFixture("identity");
            var publicKey = Crypto.Identity.PublicKey.FromString((string)fixture["data"]["publicKey"]);
            var actual = Crypto.Identity.Address.FromPublicKey(publicKey, 0x1e);

            Assert.AreEqual((string)fixture["data"]["address"], actual);
        }

        [TestMethod]
        public void Should_Get_The_Address_From_Passphrase()
        {
            var fixture = TestHelper.ReadFixture("identity");
            var actual = Crypto.Identity.Address.FromPassphrase(fixture["passphrase"], 0x1e);

            Assert.AreEqual((string)fixture["data"]["address"], actual);
        }

        [TestMethod]
        public void Should_Get_The_Address_From_Private_Key()
        {
            var fixture = TestHelper.ReadFixture("identity");
            var privateKey = Crypto.Identity.PrivateKey.FromPassphrase(fixture["passphrase"]);
            var actual = Crypto.Identity.Address.FromPrivateKey(privateKey, 0x1e);

            Assert.AreEqual((string)fixture["data"]["address"], actual);
        }

        [TestMethod]
        public void Should_Validate_The_Address()
        {
            var fixture = TestHelper.ReadFixture("identity");
            var actual = Crypto.Identity.Address.Validate((string)fixture["data"]["address"], 0x1e);

            Assert.IsTrue(actual);
        }
    }
}
