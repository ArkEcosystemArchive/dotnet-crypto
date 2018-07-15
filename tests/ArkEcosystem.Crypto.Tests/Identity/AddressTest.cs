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
            var publicKey = Crypto.Identity.PublicKey.FromString("034151a3ec46b5670a682b0a63394f863587d1bc97483b1b6c70eb58e7f0aed192");
            var actual = Crypto.Identity.Address.FromPublicKey(publicKey, 0x1e);

            Assert.AreEqual("D61mfSggzbvQgTUe6JhYKH2doHaqJ3Dyib", actual);
        }

        [TestMethod]
        public void Should_Get_The_Address_From_Secret()
        {
            var actual = Crypto.Identity.Address.FromPassphrase("this is a top secret passphrase", 0x1e);

            Assert.AreEqual("D61mfSggzbvQgTUe6JhYKH2doHaqJ3Dyib", actual);
        }

        [TestMethod]
        public void Should_Get_The_Address_From_Private_Key()
        {
            var privateKey = Crypto.Identity.PrivateKey.FromPassphrase("this is a top secret passphrase");
            var actual = Crypto.Identity.Address.FromPrivateKey(privateKey, 0x1e);

            Assert.AreEqual("D61mfSggzbvQgTUe6JhYKH2doHaqJ3Dyib", actual);
        }

        [TestMethod]
        public void Should_Validate_The_Address()
        {
            var actual = Crypto.Identity.Address.Validate("D61mfSggzbvQgTUe6JhYKH2doHaqJ3Dyib", 0x1e);

            Assert.IsTrue(actual);
        }
    }
}
