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
using ArkEcosystem.Crypto.Enums;

namespace ArkEcosystem.Crypto.Configuration
{
    public static class Fee
    {
        public static List<UInt64> fees = new List<UInt64>{
            TransactionFees.TRANSFER,
            TransactionFees.SECOND_SIGNATURE_REGISTRATION,
            TransactionFees.DELEGATE_REGISTRATION,
            TransactionFees.VOTE,
            TransactionFees.MULTI_SIGNATURE_REGISTRATION,
            TransactionFees.IPFS,
            TransactionFees.TIMELOCK_TRANSFER,
            TransactionFees.MULTI_PAYMENT,
            TransactionFees.DELEGATE_RESIGNATION,
        };

        public static UInt64 Get(int type)
        {
            return fees[type];
        }

        public static void Set(int type, UInt64 value)
        {
            fees[type] = value;
        }
    }
}
