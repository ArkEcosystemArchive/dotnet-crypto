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
    public class NetworkTest
    {
        [TestMethod]
        public void Should_Get_The_Default_Network()
        {
            var defaultNetwork = Crypto.Configuration.Network.Get();
            var expectedNetwork = new Crypto.Networks.Devnet();
            Assert.AreEqual(expectedNetwork.GetEpoch(), defaultNetwork.GetEpoch());
            Assert.AreEqual(expectedNetwork.GetVersion(), defaultNetwork.GetVersion());
            Assert.AreEqual(expectedNetwork.GetWIF(), defaultNetwork.GetWIF());
        }

        [TestMethod]
        public void Should_Set_The_Network()
        {
            var expectedNetwork = new Crypto.Networks.Mainnet();
            Crypto.Configuration.Network.Set(new Crypto.Networks.Mainnet());
            var actualNetwork = Crypto.Configuration.Network.Get();
            Assert.AreEqual(expectedNetwork.GetEpoch(), actualNetwork.GetEpoch());
            Assert.AreEqual(expectedNetwork.GetVersion(), actualNetwork.GetVersion());
            Assert.AreEqual(expectedNetwork.GetWIF(), actualNetwork.GetWIF());
        }
    }
}
