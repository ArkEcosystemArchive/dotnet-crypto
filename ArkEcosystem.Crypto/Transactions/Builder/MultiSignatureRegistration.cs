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

using System;
using System.Collections.Generic;
using ArkEcosystem.Crypto.Configuration;

namespace ArkEcosystem.Crypto.Transactions.Builder
{
    public static class MultiSignatureRegistration
    {
        public static Transaction Create(int min, int lifetime, List<string> keysgroup, string passphrase, string secondPassphrase)
        {
            var transaction = new Transaction
            {
                Type = Enums.TransactionTypes.MULTI_SIGNATURE_REGISTRATION
            };

            transaction.Asset.Add("multisignature", new Dictionary<string, dynamic>());
            transaction.Asset["multisignature"].Add("min", min);
            transaction.Asset["multisignature"].Add("lifetime", lifetime);
            transaction.Asset["multisignature"].Add("keysgroup", keysgroup);

            transaction.Fee = Convert.ToUInt64(keysgroup.Count + 1) * Fee.Get(Enums.TransactionTypes.MULTI_SIGNATURE_REGISTRATION);

            return Builder.Sign(transaction, passphrase, secondPassphrase);
        }
    }
}
