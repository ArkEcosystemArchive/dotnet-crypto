using System.Collections.Generic;

namespace ArkEcosystem.Crypto.Builder
{
    public class DelegateRegistration
    {
        public TransactionModel Create(string username, string secret, string secondSecret = null)
        {
            var transaction = new TransactionModel
            {
                Type = Enums.TransactionTypes.DELEGATE_REGISTRATION,
                Fee = Enums.TransactionFees.DELEGATE_REGISTRATION
            };
            transaction.Asset.Add("delegate", new Dictionary<string, string>());
            transaction.Asset["delegate"].Add("username", username);

            return Builder.Sign(transaction, secret, secondSecret);
        }
    }
}
