using System.IO;

namespace ArkEcosystem.Crypto.Serialisers
{
    public static class IPFS
    {
        public static void Serialise(BinaryWriter writer, TransactionModel transaction)
        {
            writer.Write((byte)(transaction.Asset["ipfs"]["dag"].length / 2));
            writer.Write(transaction.Asset["ipfs"]["dag"]);
        }
    }
}
