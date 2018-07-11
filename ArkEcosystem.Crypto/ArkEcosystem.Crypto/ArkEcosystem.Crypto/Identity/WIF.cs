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
using NBitcoin.DataEncoders;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace ArkEcosystem.Crypto.Identity
{
    public static class WIF
    {
        static readonly SHA256 Sha256 = SHA256.Create();

        public static string FromPassphrase(string passphrase)
        {
            MemoryStream stream = new MemoryStream();

            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(Configuration.Network.Get().GetWIF());
                writer.Write(Sha256.ComputeHash(Encoding.ASCII.GetBytes(passphrase)));
                writer.Write((byte)0x01);

                return Encoders.Base58Check.EncodeData(stream.ToArray());
            }
        }
    }
}
