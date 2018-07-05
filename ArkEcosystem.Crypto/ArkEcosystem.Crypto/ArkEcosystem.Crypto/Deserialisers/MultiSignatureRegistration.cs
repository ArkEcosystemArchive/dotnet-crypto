using System.Collections.Generic;
using System.IO;

namespace ArkEcosystem.Crypto.Deserialisers
{
    public static class MultiSignatureRegistration
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

            transaction.Asset.Add("min", reader.ReadByte() & 0xff);
            var count = reader.ReadByte() & 0xff;
            transaction.Asset.Add("lifetime", reader.ReadByte() & 0xff);

            transaction.Asset.Add("keysgroup", new List<string>());
            for (int i = 0; i < count; i++)
            {
                var key = serialised.Substring(assetOffset + 6 + i * 66, 66);

                transaction.Asset["keysgroup"].Add(key);
            }

            return transaction.ParseSignatures(serialised, assetOffset + 6 + count * 66);
        }
    }
}
