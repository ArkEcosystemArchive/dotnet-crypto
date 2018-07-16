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

namespace ArkEcosystem.Crypto.Enums
{
    public static class TransactionTypes
    {
        public static readonly byte TRANSFER = 0;
        public static readonly byte SECOND_SIGNATURE_REGISTRATION = 1;
        public static readonly byte DELEGATE_REGISTRATION = 2;
        public static readonly byte VOTE = 3;
        public static readonly byte MULTI_SIGNATURE_REGISTRATION = 4;
        public static readonly byte IPFS = 5;
        public static readonly byte TIMELOCK_TRANSFER = 6;
        public static readonly byte MULTI_PAYMENT = 7;
        public static readonly byte DELEGATE_RESIGNATION = 8;
    }
}
