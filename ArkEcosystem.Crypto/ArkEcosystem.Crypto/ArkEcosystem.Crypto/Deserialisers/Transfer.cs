using NBitcoin.DataEncoders;
using System.IO;

namespace ArkEcosystem.Crypto.Deserialisers
{
    public static class Transfer
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

            transaction.Amount = reader.ReadUInt64();
            transaction.Expiration = reader.ReadUInt32();
            transaction.RecipientId = Encoders.Base58Check.EncodeData(reader.ReadBytes(21));

            return transaction.ParseSignatures(serialised, assetOffset + (21 + 12) * 2);
        }
    }
}
