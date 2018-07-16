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
using System;
using System.IO;

namespace ArkEcosystem.Crypto
{
    public class Serializer
    {
        readonly TransactionModel transaction;
        readonly MemoryStream stream;
        readonly BinaryWriter writer;

        public Serializer(TransactionModel transaction)
        {
            this.transaction = transaction;
            stream = new MemoryStream();
            writer = new BinaryWriter(stream);
        }

        public byte[] Serialize()
        {
            HandleHeader();
            HandleTypeSpecific();
            HandleSignatures();

            return stream.ToArray();
        }

        public void HandleHeader()
        {
            writer.Write(transaction.Header);
            writer.Write(transaction.Version);
            writer.Write(transaction.Network);
            writer.Write(transaction.Type);
            writer.Write(transaction.Timestamp);
            writer.Write(Encoders.Hex.DecodeData(transaction.SenderPublicKey));
            writer.Write(transaction.Fee);

            if (transaction.VendorField != null)
            {
                // writer.Write((byte)transaction.VendorField.Length);
                writer.Write(transaction.VendorField);
            }
            else if (transaction.VendorFieldHex != null)
            {
                writer.Write((byte)(transaction.VendorFieldHex.Length / 2));
                writer.Write(transaction.VendorFieldHex);
            }
            else
            {
                writer.Write((byte)0x00);
            }
        }

        private void HandleTypeSpecific()
        {
            switch (transaction.Type)
            {
                case 0:
                    Serializers.Transfer.Serialize(writer, transaction);
                    break;
                case 1:
                    Serializers.SecondSignatureRegistration.Serialize(writer, transaction);
                    break;
                case 2:
                    Serializers.DelegateRegistration.Serialize(writer, transaction);
                    break;
                case 3:
                    Serializers.Vote.Serialize(writer, transaction);
                    break;
                case 4:
                    Serializers.MultiSignatureRegistration.Serialize(writer, transaction);
                    break;
                case 5:
                    Serializers.IPFS.Serialize(writer, transaction);
                    break;
                case 6:
                    Serializers.TimelockTransfer.Serialize(writer, transaction);
                    break;
                case 7:
                    Serializers.MultiPayment.Serialize(writer, transaction);
                    break;
                case 8:
                    Serializers.DelegateResignation.Serialize(writer, transaction);
                    break;
            }
        }

        void HandleSignatures()
        {
            if (transaction.Signature != null)
            {
                writer.Write(Encoders.Hex.DecodeData(transaction.Signature));
            }

            if (transaction.SecondSignature != null)
            {
                writer.Write(Encoders.Hex.DecodeData(transaction.SecondSignature));
            }
            else if (transaction.SignSignature != null)
            {
                writer.Write(Encoders.Hex.DecodeData(transaction.SignSignature));
            }

            if (transaction.Signatures != null)
            {
                writer.Write((byte)0xff);
                writer.Write(Encoders.Hex.DecodeData(String.Join("", transaction.Signatures)));
            }
        }
    }
}
