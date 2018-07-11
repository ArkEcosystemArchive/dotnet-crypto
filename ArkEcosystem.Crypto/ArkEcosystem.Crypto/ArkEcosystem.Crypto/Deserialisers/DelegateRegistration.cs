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

namespace ArkEcosystem.Crypto.Deserialisers
{
    public static class DelegateRegistration
    {
        public static TransactionModel Deserialise(
            BinaryReader reader,
            MemoryStream stream,
            TransactionModel transaction,
            string serialised,
            int assetOffset
        )
        {
            stream.Position = assetOffset / 2;

            var usernameLength = reader.ReadByte() & 0xff;
            var username = serialised.Substring((assetOffset / 2 + 1) * 2, usernameLength * 2);

            transaction.Asset.Add("delegate", new Dictionary<string, string>());
            transaction.Asset["delegate"].Add("username", ConvertHexToString(username));

            return transaction.ParseSignatures(serialised, assetOffset + (usernameLength + 1) * 2);
        }

        private static string ConvertHexToString(string HexValue)
        {
            string StrValue = "";

            while (HexValue.Length > 0)
            {
                StrValue += System.Convert.ToChar(System.Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
                HexValue = HexValue.Substring(2, HexValue.Length - 2);
            }

            return StrValue;
        }
    }
}
