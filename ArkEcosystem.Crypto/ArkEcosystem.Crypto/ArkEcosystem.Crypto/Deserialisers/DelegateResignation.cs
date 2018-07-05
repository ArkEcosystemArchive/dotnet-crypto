using System.IO;

namespace ArkEcosystem.Crypto.Deserialisers
{
    public static class DelegateResignation
    {
        public static TransactionModel Deserialise(
            BinaryReader reader,
            MemoryStream stream,
            TransactionModel transaction,
            string serialised,
            int assetOffset
        ) {
            return transaction.ParseSignatures(serialised, assetOffset);
        }
    }
}
