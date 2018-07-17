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
using System.Collections.Generic;
using System.IO;

namespace ArkEcosystem.Crypto.Transactions.Deserializers
{
    public static class MultiPayment
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

            var total = reader.ReadUInt16() & 0xff;
            var offset = assetOffset / 2 + 1;

            transaction.Asset.Add("payments", new List<Dictionary<string, dynamic>>());
            for (int i = 0; i < total; i++)
            {
                stream.Position += offset;

                Dictionary<string, dynamic> payment = new Dictionary<string, dynamic>
                {
                    ["amount"] = reader.ReadUInt64(),
                    ["recipientId"] = Encoders.Base58Check.EncodeData(reader.ReadBytes(21))
                };

                transaction.Asset["payments"].Add(payment);

                offset += 22;
            }

            foreach (var payment in transaction.Asset["payments"])
            {
                transaction.Amount += payment["amount"];
            }

            return transaction.ParseSignatures(serialized, offset * 2);
        }
    }
}
