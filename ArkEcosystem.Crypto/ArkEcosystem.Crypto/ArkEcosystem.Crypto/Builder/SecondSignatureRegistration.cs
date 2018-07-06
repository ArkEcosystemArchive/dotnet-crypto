using System.Collections.Generic;
using NBitcoin.DataEncoders;

namespace ArkEcosystem.Crypto.Builder
{
    public class SecondSignatureRegistration
    {
        public static TransactionModel Create(string secret, string secondSecret)
        {
            var transaction = new TransactionModel
            {
                Type = Enums.TransactionTypes.SECOND_SIGNATURE_REGISTRATION,
                Fee = Enums.TransactionFees.SECOND_SIGNATURE_REGISTRATION
            };

            transaction.Asset.Add("signature", new Dictionary<string, string>());
            var publicKey = Encoders.Hex.EncodeData(Identity.PublicKey.FromSecret(secondSecret).ToBytes());
            transaction.Asset["signature"].Add("publicKey", publicKey);

            return Builder.Sign(transaction, secret, secondSecret);
        }
    }
}
