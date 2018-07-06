namespace ArkEcosystem.Crypto.Builder
{
    public class Transfer
    {
        public static TransactionModel Create(string recipientId, ulong amount, string vendorField, string secret, string secondSecret = null)
        {
            var transaction = new TransactionModel
            {
                Type = Enums.TransactionTypes.TRANSFER,
                Fee = Enums.TransactionFees.TRANSFER,
                RecipientId = recipientId,
                Amount = amount,
                VendorField = vendorField
            };

            return Builder.Sign(transaction, secret, secondSecret);
        }
    }
}
