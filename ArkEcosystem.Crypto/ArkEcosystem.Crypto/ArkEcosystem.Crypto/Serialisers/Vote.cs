using System.IO;

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
                    voteBytes += "01" + vote;
                }
                else
                {
                    voteBytes += "00" + vote;
                }
            }

            writer.Write((byte)transaction.Asset["votes"].count);
            writer.Write(voteBytes);
        }
    }
}
