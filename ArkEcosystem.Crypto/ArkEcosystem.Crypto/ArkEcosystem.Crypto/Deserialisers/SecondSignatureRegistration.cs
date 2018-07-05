using System.Collections.Generic;
using System.IO;

namespace ArkEcosystem.Crypto.Deserialisers
{
    public static class SecondSignatureRegistration
    {
        public static TransactionModel Deserialise(
            BinaryReader reader,
            MemoryStream stream,
            TransactionModel transaction,
            string serialised,
            int assetOffset
        )
        {
            transaction.Asset.Add("signature", new Dictionary<string, string>());
            transaction.Asset["signature"].Add("publicKey", serialised.Substring(assetOffset, 66));

            return transaction.ParseSignatures(serialised, assetOffset + 66);
        }
    }
}
