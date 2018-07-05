using NBitcoin.DataEncoders;
using System.IO;

namespace ArkEcosystem.Crypto.Serialisers
{
    public static class TimelockTransfer
    {
        public static void Serialise(BinaryWriter writer, TransactionModel transaction)
        {
            writer.Write(transaction.Amount);
            writer.Write(transaction.TimelockType);
            writer.Write(transaction.Timelock);
            writer.Write(Encoders.Base58Check.DecodeData(transaction.RecipientId));
        }
    }
}
