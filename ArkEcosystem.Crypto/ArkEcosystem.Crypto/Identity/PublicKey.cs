using NBitcoin;

namespace ArkEcosystem.Crypto.Identity
{
    public static class PublicKey
    {
        public static PubKey FromSecret(string secret)
        {
            return PrivateKey.FromSecret(secret).PubKey;
        }

        public static PubKey FromString(string hex)
        {
            return new PubKey(hex);
        }
    }
}
