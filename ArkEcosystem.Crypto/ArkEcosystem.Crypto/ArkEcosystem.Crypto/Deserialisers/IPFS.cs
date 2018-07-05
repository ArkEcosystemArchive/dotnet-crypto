using System.IO;

namespace ArkEcosystem.Crypto.Deserialisers
{
    public static class IPFS
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

            var length = reader.ReadByte() & 0xff;
            transaction.Asset.Add("dag", serialised.Substring(assetOffset + 2, length * 2));

            return transaction.ParseSignatures(serialised, assetOffset + 2 + length * 2);
        }
    }
}
