namespace ArkEcosystem.Crypto.Builder
{
    public class Builder
    {
        public static TransactionModel Sign(TransactionModel transaction, string secret, string secondSecret = null)
        {
            transaction.Timestamp = Slot.GetTime();

            transaction.Signature = transaction.Sign(secret);

            if (secondSecret != null)
            {
                transaction.SignSignature = transaction.SecondSign(secondSecret);
            }

            transaction.Id = transaction.GetId();

            return transaction;
        }
    }
}
