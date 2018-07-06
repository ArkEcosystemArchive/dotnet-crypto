using System.IO;
using NBitcoin.DataEncoders;

namespace ArkEcosystem.Crypto.Serialisers
{
    public static class MultiSignatureRegistration
    {
        public static void Serialise(BinaryWriter writer, TransactionModel transaction)
        {
            var joined = "";

            if (transaction.Version == 1)
            {
                foreach (var key in transaction.Asset["multisignature"]["keysgroup"])
                {
                    joined += key.Substring(1);
                }
            }
            else
            {
                joined = string.Join("", transaction.Asset["multisignature"]["keysgroup"]);
            }

            writer.Write((byte)transaction.Asset["multisignature"]["min"]);
            writer.Write((byte)transaction.Asset["multisignature"]["keysgroup"].Count);
            writer.Write((byte)transaction.Asset["multisignature"]["lifetime"]);
            writer.Write(Encoders.Hex.DecodeData(joined));
        }
    }
}
