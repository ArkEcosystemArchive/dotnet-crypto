using System.Text;
using NBitcoin;
using System.Security.Cryptography;

namespace ArkEcosystem.Crypto.Identity
{
    public static class PrivateKey
    {
        static readonly SHA256 Sha256 = SHA256.Create();

        public static Key FromSecret(string secret)
        {
            var privateKeyHash = Sha256.ComputeHash(Encoding.ASCII.GetBytes(secret));

            return new Key(privateKeyHash);
        }
    }
}
