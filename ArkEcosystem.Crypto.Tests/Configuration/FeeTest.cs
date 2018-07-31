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

namespace ArkEcosystem.Crypto.Tests.Configuration
{
    [TestClass]
    public class FeeTest
    {
        [TestMethod]
        public void Should_Get_The_Default_Fees()
        {
            var fee = Crypto.Configuration.Fee.Get(0);
            Assert.AreEqual(Enums.TransactionFees.TRANSFER, fee);
            fee = Crypto.Configuration.Fee.Get(1);
            Assert.AreEqual(Enums.TransactionFees.SECOND_SIGNATURE_REGISTRATION, fee);
            fee = Crypto.Configuration.Fee.Get(2);
            Assert.AreEqual(Enums.TransactionFees.DELEGATE_REGISTRATION, fee);
            fee = Crypto.Configuration.Fee.Get(3);
            Assert.AreEqual(Enums.TransactionFees.VOTE, fee);
            fee = Crypto.Configuration.Fee.Get(4);
            Assert.AreEqual(Enums.TransactionFees.MULTI_SIGNATURE_REGISTRATION, fee);
            fee = Crypto.Configuration.Fee.Get(5);
            Assert.AreEqual(Enums.TransactionFees.IPFS, fee);
            fee = Crypto.Configuration.Fee.Get(6);
            Assert.AreEqual(Enums.TransactionFees.TIMELOCK_TRANSFER, fee);
            fee = Crypto.Configuration.Fee.Get(7);
            Assert.AreEqual(Enums.TransactionFees.MULTI_PAYMENT, fee);
            fee = Crypto.Configuration.Fee.Get(8);
            Assert.AreEqual(Enums.TransactionFees.DELEGATE_RESIGNATION, fee);
        }

        [TestMethod]
        public void Should_Set_The_Fees()
        {
            Crypto.Configuration.Fee.Set(0, 1);
            Assert.AreEqual(1ul, Crypto.Configuration.Fee.Get(0));
            Crypto.Configuration.Fee.Set(1, 2);
            Assert.AreEqual(2ul, Crypto.Configuration.Fee.Get(1));
            Crypto.Configuration.Fee.Set(2, 3);
            Assert.AreEqual(3ul, Crypto.Configuration.Fee.Get(2));
            Crypto.Configuration.Fee.Set(3, 4);
            Assert.AreEqual(4ul, Crypto.Configuration.Fee.Get(3));
            Crypto.Configuration.Fee.Set(4, 5);
            Assert.AreEqual(5ul, Crypto.Configuration.Fee.Get(4));
            Crypto.Configuration.Fee.Set(5, 6);
            Assert.AreEqual(6ul, Crypto.Configuration.Fee.Get(5));
            Crypto.Configuration.Fee.Set(6, 7);
            Assert.AreEqual(7ul, Crypto.Configuration.Fee.Get(6));
            Crypto.Configuration.Fee.Set(7, 8);
            Assert.AreEqual(8ul, Crypto.Configuration.Fee.Get(7));
            Crypto.Configuration.Fee.Set(8, 9);
            Assert.AreEqual(9ul, Crypto.Configuration.Fee.Get(8));
        }
    }
}
