using System.Collections.Generic;

namespace ArkEcosystem.Crypto.Builder
{
    public class Vote
    {
        public static TransactionModel Create(List<string> votes, string secret, string secondSecret = null)
        {
            var transaction = new TransactionModel
            {
                Type = Enums.TransactionTypes.VOTE,
                Fee = Enums.TransactionFees.VOTE
            };
            transaction.Asset.Add("votes", votes);
            transaction.RecipientId = Identity.Address.FromSecret(secret);

            return Builder.Sign(transaction, secret, secondSecret);
        }
    }
}
