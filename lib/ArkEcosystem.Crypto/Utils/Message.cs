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
using NBitcoin.Crypto;
using NBitcoin.DataEncoders;
using NBitcoin;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ArkEcosystem.Crypto
{
    public class Message
    {
        static readonly System.Security.Cryptography.SHA256 Sha256 = System.Security.Cryptography.SHA256.Create();
        public readonly string publicKey;
        public readonly string signature;
        public readonly string message;

        public Message(string publicKey, string signature, string message)
        {
            this.publicKey = publicKey;
            this.signature = signature;
            this.message = message;
        }

        public static Message Sign(string message, string passphrase)
        {
            var privateKeyBytes = Sha256.ComputeHash(Encoding.ASCII.GetBytes(passphrase));
            var privateKey = new Key(privateKeyBytes);

            var messageBytes = Sha256.ComputeHash(Encoding.ASCII.GetBytes(message));
            var signature = privateKey.Sign(new uint256(messageBytes));

            return new Message(
                privateKey.PubKey.ToString(),
                Encoders.Hex.EncodeData(signature.ToDER()),
                message
            );
        }

        public bool Verify()
        {
            var messageBytes = Sha256.ComputeHash(Encoding.ASCII.GetBytes(message));
            var messageSignature = ECDSASignature.FromDER(Encoders.Hex.DecodeData(signature));

            return new PubKey(publicKey).Verify(new uint256(messageBytes), messageSignature);
        }

        public Dictionary<string, string> ToDictionary()
        {
            return new Dictionary<string, string>
            {
                { "publickey", publicKey },
                { "signature", signature },
                { "message", message }
            };
        }

        public string ToJson()
        {
            return JObject.FromObject(ToDictionary()).ToString();
        }
    }
}
