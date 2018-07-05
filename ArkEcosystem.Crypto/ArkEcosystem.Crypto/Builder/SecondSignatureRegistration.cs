using NBitcoin.DataEncoders;

namespace ArkEcosystem.Crypto.Builder
{
    public class SecondSignatureRegistration
    {
        public TransactionModel Create(string secondSecret, string secret)
        {
            var transaction = new TransactionModel
            {
                Type = Enums.TransactionTypes.SECOND_SIGNATURE_REGISTRATION,
                Fee = Enums.TransactionFees.SECOND_SIGNATURE_REGISTRATION,
                Signature = Encoders.Hex.EncodeData(Identity.PublicKey.FromSecret(secondSecret).ToBytes())
            };

            return Builder.Sign(transaction, secret, secondSecret);
        }
    }
}
