using NBitcoin.DataEncoders;
using System.IO;

namespace ArkEcosystem.Crypto.Serialisers
{
    public static class SecondSignatureRegistration
    {
        public static void Serialise(BinaryWriter writer, TransactionModel transaction)
        {
            writer.Write(Encoders.Base58Check.DecodeData(transaction.Asset["signature"]["publicKey"]));
        }
    }
}
