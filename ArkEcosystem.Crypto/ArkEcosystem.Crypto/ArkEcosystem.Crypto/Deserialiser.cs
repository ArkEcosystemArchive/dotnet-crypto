using NBitcoin.DataEncoders;
using System.Text;
using System.IO;

namespace ArkEcosystem.Crypto
{
    public class Deserialiser
    {
        string serialised;
        MemoryStream stream;
        BinaryReader reader;
        int assetOffset;

        public Deserialiser(string serialised)
        {
            this.serialised = serialised;
            stream = new MemoryStream(Encoders.Hex.DecodeData(serialised));
            reader = new BinaryReader(stream);
        }

        public TransactionModel Deserialise()
        {
            var transaction = new TransactionModel();
            transaction = HandleHeader(transaction);
            transaction = HandleType(transaction);

            if (transaction.Type == 1)
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
                transaction.VendorFieldHex = serialised.Substring((41 + 8 + 1) * 2, vendorFieldLength * 2);
            }

            assetOffset = (41 + 8 + 1) * 2 + vendorFieldLength * 2;

            return transaction;
        }

        TransactionModel HandleType(TransactionModel transaction)
        {
            switch (transaction.Type)
            {
                case 0:
                    transaction = Deserialisers.Transfer.Deserialise(reader, stream, transaction, serialised, assetOffset);
                    break;
                case 1:
                    transaction = Deserialisers.SecondSignatureRegistration.Deserialise(reader, stream, transaction, serialised, assetOffset);
                    break;
                case 2:
                    transaction = Deserialisers.DelegateRegistration.Deserialise(reader, stream, transaction, serialised, assetOffset);
                    break;
                case 3:
                    transaction = Deserialisers.Vote.Deserialise(reader, stream, transaction, serialised, assetOffset);
                    break;
                case 4:
                    transaction = Deserialisers.MultiSignatureRegistration.Deserialise(reader, stream, transaction, serialised, assetOffset);
                    break;
                case 5:
                    transaction = Deserialisers.IPFS.Deserialise(reader, stream, transaction, serialised, assetOffset);
                    break;
                case 6:
                    transaction = Deserialisers.TimelockTransfer.Deserialise(reader, stream, transaction, serialised, assetOffset);
                    break;
                case 7:
                    transaction = Deserialisers.MultiPayment.Deserialise(reader, stream, transaction, serialised, assetOffset);
                    break;
                case 8:
                    transaction = Deserialisers.DelegateResignation.Deserialise(reader, stream, transaction, serialised, assetOffset);
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
                // var publicKey = Identity.PublicKey.FromString(transaction.senderPublicKey);
                // transaction.RecipientId = Identity.Address.FromPublicKey(publicKey, transaction.Network);
            }

            if (transaction.Type == 1)
            {
                // var publicKey = Identity.PublicKey.FromString(transaction.senderPublicKey);
                // transaction.RecipientId = Identity.Address.FromPublicKey(publicKey, transaction.Network);
            }

            if (transaction.VendorFieldHex != null)
            {
                transaction.VendorField = Encoding.UTF8.GetString(Encoders.Hex.DecodeData(transaction.VendorFieldHex));
            }

            if (transaction.Type == 4)
            {
                // var publicKey = Identity.PublicKey.FromString(transaction.senderPublicKey);
                // transaction.RecipientId = Identity.Address.FromPublicKey(publicKey, transaction.Network);

                for (int i = 0; i < transaction.Asset["multisignature"]["keysgroup"].Count; i++)
                {
                    transaction.Asset["multisignature"]["keysgroup"][i] = "+" + transaction.Asset["multisignature"]["keysgroup"][i];
                }
            }

            if (transaction.Id == null)
            {
                transaction.Id = transaction.GetId();
            }

            return transaction;
        }
    }
}
