using NBitcoin.DataEncoders;
using System.IO;

namespace ArkEcosystem.Crypto.Serialisers
{
    public static class MultiPayment
    {
        public static void Serialise(BinaryWriter writer, TransactionModel transaction)
        {
            writer.Write(transaction.Asset["payments"].Count);

            foreach (var payment in transaction.Asset["payments"])
            {
                writer.Write(payment["amount"]);
                writer.Write(Encoders.Base58Check.DecodeData(payment["recipientId"]));
            }
        }
    }
}
