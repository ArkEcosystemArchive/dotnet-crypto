using System.IO;
using NBitcoin.DataEncoders;

namespace ArkEcosystem.Crypto.Serialisers
{
    public static class Vote
    {
        public static void Serialise(BinaryWriter writer, TransactionModel transaction)
        {
            var voteBytes = "";

            foreach (var vote in transaction.Asset["votes"])
            {
                if (vote.StartsWith('+'))
                {
                    voteBytes += "01" + vote.Substring(1);
                }
                else
                {
                    voteBytes += "00" + vote.Substring(1);
                }
            }

            writer.Write((byte)transaction.Asset["votes"].Count);
            writer.Write(Encoders.Hex.DecodeData(voteBytes));
        }
    }
}
