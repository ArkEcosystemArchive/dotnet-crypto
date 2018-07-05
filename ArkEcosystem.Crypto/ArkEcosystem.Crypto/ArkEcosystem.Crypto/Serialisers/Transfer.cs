using NBitcoin.DataEncoders;
using System.IO;

namespace ArkEcosystem.Crypto.Serialisers
{
    public static class Transfer
    {
        public static void Serialise(BinaryWriter writer, TransactionModel transaction)
        {
            writer.Write(transaction.Amount);
            writer.Write(transaction.Expiration);
            writer.Write(Encoders.Base58Check.DecodeData(transaction.RecipientId));
        }
    }
}
