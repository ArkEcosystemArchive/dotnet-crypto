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

namespace ArkEcosystem.Crypto.Tests.Transactions.Builder
{
    [TestClass]
    public class TransferTest
    {
        [TestMethod]
        public void Should_Create_Transaction_With_Secret()
        {
            var transaction = Crypto.Transactions.Builder.Transfer.Create(
                "AXoXnFi4z1Z6aFvjEYkDVCtBGW2PaRiM25",
                133380000000,
                "This is a transaction from .NET",
                "This is a top secret passphrase"
            );

            Assert.IsTrue(transaction.Verify());
        }

        [TestMethod]
        public void Should_Create_Transaction_With_Second_Secret()
        {
            var transaction = Crypto.Transactions.Builder.Transfer.Create(
                "AXoXnFi4z1Z6aFvjEYkDVCtBGW2PaRiM25",
                133380000000,
                "This is a transaction from .NET",
                "This is a top secret passphrase",
                "this is a top secret second passphrase"
            );

            Assert.IsTrue(transaction.Verify());
        }
    }
}
