using NBitcoin.DataEncoders;
using System;
using System.IO;

namespace ArkEcosystem.Crypto
{
    public class Serialiser
    {
        TransactionModel transaction;
        MemoryStream stream;
        BinaryWriter writer;

        public Serialiser(TransactionModel transaction)
        {
            this.transaction = transaction;
            stream = new MemoryStream();
            writer = new BinaryWriter(stream);
        }

        public byte[] Serialise()
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

            if (transaction.Type == 0)
            {
                writer.Write(transaction.Amount);
                writer.Write(transaction.Expiration);
                writer.Write(Encoders.Base58Check.DecodeData(transaction.RecipientId));
            }
        }

        private void HandleTypeSpecific()
        {
            switch (transaction.Type)
            {
                case 0:
                    Serialisers.Transfer.Serialise(writer, transaction);
                    break;
                case 1:
                    Serialisers.SecondSignatureRegistration.Serialise(writer, transaction);
                    break;
                case 2:
                    Serialisers.DelegateRegistration.Serialise(writer, transaction);
                    break;
                case 3:
                    Serialisers.Vote.Serialise(writer, transaction);
                    break;
                case 4:
                    Serialisers.MultiSignatureRegistration.Serialise(writer, transaction);
                    break;
                case 5:
                    Serialisers.IPFS.Serialise(writer, transaction);
                    break;
                case 6:
                    Serialisers.TimelockTransfer.Serialise(writer, transaction);
                    break;
                case 7:
                    Serialisers.MultiPayment.Serialise(writer, transaction);
                    break;
                case 8:
                    Serialisers.DelegateResignation.Serialise(writer, transaction);
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
                writer.Write((byte)0x00);
                writer.Write(Encoders.Hex.DecodeData(String.Join("", transaction.Signatures)));
            }
        }
    }
}
