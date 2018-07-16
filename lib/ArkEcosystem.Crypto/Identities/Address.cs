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
using NBitcoin;
using SshNet.Security.Cryptography;
using System.IO;

namespace ArkEcosystem.Crypto.Identities
{
    public static class Address
    {
        static readonly RIPEMD160 Ripemd160 = new RIPEMD160();

        public static string FromPassphrase(string passphrase, byte publicKeyHash = 0)
        {
            return FromPrivateKey(PrivateKey.FromPassphrase(passphrase), publicKeyHash);
        }

        public static string FromPublicKey(PubKey publicKey, byte publicKeyHash = 0)
        {
            MemoryStream stream = new MemoryStream();

            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                var bytes = publicKey.ToBytes();

                if (publicKeyHash != 0)
                {
                    writer.Write(publicKeyHash);
                } else {
                    writer.Write(Configuration.Network.Get().GetVersion());
                }

                writer.Write(Ripemd160.ComputeHash(bytes, 0, bytes.Length));

                return Encoders.Base58Check.EncodeData(stream.ToArray());
            }
        }

        public static string FromPrivateKey(Key privateKey, byte publicKeyHash = 0)
        {
            return FromPublicKey(privateKey.PubKey, publicKeyHash);
        }

        public static bool Validate(string address, byte publicKeyHash = 0)
        {
            var addressPrefix = Encoders.Base58Check.DecodeData(address)[0];

            if (publicKeyHash != 0)
            {
                return addressPrefix == publicKeyHash;
            }
            else
            {
                return addressPrefix == Configuration.Network.Get().GetVersion();
            }
        }
    }
}
