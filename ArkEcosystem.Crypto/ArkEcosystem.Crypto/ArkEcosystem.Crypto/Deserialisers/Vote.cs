using System.Collections.Generic;
using System.IO;

namespace ArkEcosystem.Crypto.Deserialisers
{
    public static class Vote
    {
        public static TransactionModel Deserialise(
            BinaryReader reader,
            MemoryStream stream,
            TransactionModel transaction,
            string serialised,
            int assetOffset
        )
        {
            stream.Position = assetOffset / 2;

            var voteLength = reader.ReadByte() & 0xff;

            transaction.Asset.Add("votes", new List<string>());
            for (int i = 0; i < voteLength; i++)
            {
                var vote = serialised.Substring(assetOffset + 2 + i * 2 * 34, 68);
                vote = (vote[1] == '1' ? '+' : '-') + vote.Substring(2);

                transaction.Asset["votes"].Add(vote);
            }

            return transaction.ParseSignatures(serialised, assetOffset + 2 + voteLength * 34 * 2);
        }
    }
}
