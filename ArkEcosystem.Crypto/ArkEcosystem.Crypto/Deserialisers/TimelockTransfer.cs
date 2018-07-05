using NBitcoin.DataEncoders;
using System.IO;

namespace ArkEcosystem.Crypto.Deserialisers
{
    public static class TimelockTransfer
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
            transaction.TimelockType = reader.ReadByte() & 0xff;
            transaction.Timelock = reader.ReadUInt64();
            transaction.RecipientId = Encoders.Base58Check.EncodeData(reader.ReadBytes(21));

            return transaction.ParseSignatures(serialised, assetOffset + (21 + 13) * 2);
        }
    }
}
