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

namespace ArkEcosystem.Crypto.Transactions
{
    public class Deserializer
    {
        string serialized;
        MemoryStream stream;
        BinaryReader reader;
        int assetOffset;

        public Deserializer(string serialized)
        {
            this.serialized = serialized;
            stream = new MemoryStream(Encoders.Hex.DecodeData(serialized));
            reader = new BinaryReader(stream);
        }

        public TransactionModel Deserialize()
        {
            var transaction = new TransactionModel();
            transaction = HandleHeader(transaction);
            transaction = HandleType(transaction);

            if (transaction.Version == 1)
            {
                transaction = HandleVersionOne(transaction);
            }

            return transaction;
        }

        public TransactionModel HandleHeader(TransactionModel transaction)
        {
            transaction.Header = reader.ReadByte();
            transaction.Version = reader.ReadByte();
            transaction.Network = reader.ReadByte();
            transaction.Type = reader.ReadByte();
            transaction.Timestamp = reader.ReadUInt32();
            transaction.SenderPublicKey = Encoders.Hex.EncodeData(reader.ReadBytes(33));
            transaction.Fee = reader.ReadUInt64();

            var vendorFieldLength = reader.ReadByte();

            if (vendorFieldLength > 0)
            {
                transaction.VendorFieldHex = serialized.Substring((41 + 8 + 1) * 2, vendorFieldLength * 2);
            }

            assetOffset = (41 + 8 + 1) * 2 + vendorFieldLength * 2;

            return transaction;
        }

        TransactionModel HandleType(TransactionModel transaction)
        {
            switch (transaction.Type)
            {
                case 0:
                    transaction = Deserializers.Transfer.Deserialize(reader, stream, transaction, serialized, assetOffset);
                    break;
                case 1:
                    transaction = Deserializers.SecondSignatureRegistration.Deserialize(reader, stream, transaction, serialized, assetOffset);
                    break;
                case 2:
                    transaction = Deserializers.DelegateRegistration.Deserialize(reader, stream, transaction, serialized, assetOffset);
                    break;
                case 3:
                    transaction = Deserializers.Vote.Deserialize(reader, stream, transaction, serialized, assetOffset);
                    break;
                case 4:
                    transaction = Deserializers.MultiSignatureRegistration.Deserialize(reader, stream, transaction, serialized, assetOffset);
                    break;
                case 5:
                    transaction = Deserializers.IPFS.Deserialize(reader, stream, transaction, serialized, assetOffset);
                    break;
                case 6:
                    transaction = Deserializers.TimelockTransfer.Deserialize(reader, stream, transaction, serialized, assetOffset);
                    break;
                case 7:
                    transaction = Deserializers.MultiPayment.Deserialize(reader, stream, transaction, serialized, assetOffset);
                    break;
                case 8:
                    transaction = Deserializers.DelegateResignation.Deserialize(reader, stream, transaction, serialized, assetOffset);
                    break;
            }

            return transaction;
        }

        TransactionModel HandleVersionOne(TransactionModel transaction)
        {
            if (transaction.SecondSignature != "")
            {
                transaction.SignSignature = transaction.SecondSignature;
            }

            if (transaction.Type == 3)
            {
                var publicKey = Identity.PublicKey.FromHex(transaction.SenderPublicKey);
                transaction.RecipientId = Identity.Address.FromPublicKey(publicKey, transaction.Network);
            }

            if (transaction.Type == 4)
            {
                for (int i = 0; i < transaction.Asset["multisignature"]["keysgroup"].Count; i++)
                {
                    transaction.Asset["multisignature"]["keysgroup"][i] = "+" + transaction.Asset["multisignature"]["keysgroup"][i];
                }
            }

            if (transaction.VendorFieldHex != null)
            {
                transaction.VendorField = Encoding.UTF8.GetString(Encoders.Hex.DecodeData(transaction.VendorFieldHex));
            }

            if (transaction.Id == null)
            {
                transaction.Id = transaction.GetId();
            }

            if (transaction.Type == 1 || transaction.Type == 4)
            {
                var publicKey = Identity.PublicKey.FromHex(transaction.SenderPublicKey);
                transaction.RecipientId = Identity.Address.FromPublicKey(publicKey, transaction.Network);
            }

            return transaction;
        }
    }
}
