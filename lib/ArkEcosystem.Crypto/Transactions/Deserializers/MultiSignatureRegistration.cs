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

using System.Collections.Generic;
using System.IO;

namespace ArkEcosystem.Crypto.Transactions.Deserializers
{
    public static class MultiSignatureRegistration
    {
        public static Transaction Deserialize(
            BinaryReader reader,
            MemoryStream stream,
            Transaction transaction,
            string serialized,
            int assetOffset
        )
        {
            stream.Position = assetOffset / 2;

            transaction.Asset.Add("multisignature", new Dictionary<string, dynamic>());
            transaction.Asset["multisignature"].Add("min", reader.ReadByte() & 0xff);
            var count = reader.ReadByte() & 0xff;
            transaction.Asset["multisignature"].Add("lifetime", reader.ReadByte() & 0xff);

            transaction.Asset["multisignature"].Add("keysgroup", new List<string>());
            for (int i = 0; i < count; i++)
            {
                var key = serialized.Substring(assetOffset + 6 + i * 66, 66);

                transaction.Asset["multisignature"]["keysgroup"].Add(key);
            }

            return transaction.ParseSignatures(serialized, assetOffset + 6 + count * 66);
        }
    }
}
