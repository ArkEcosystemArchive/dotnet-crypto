using NBitcoin.DataEncoders;
using NBitcoin;
using SshNet.Security.Cryptography;
using System.IO;

namespace ArkEcosystem.Crypto.Identity
{
    public static class Address
    {
        static readonly RIPEMD160 Ripemd160 = new RIPEMD160();

        public static string FromSecret(string secret)
        {
            return FromPrivateKey(PrivateKey.FromSecret(secret));
        }

        public static string FromPublicKey(PubKey publicKey)
        {
            MemoryStream stream = new MemoryStream();

            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                var bytes = publicKey.ToBytes();
                var publicKeyHash = Ripemd160.ComputeHash(bytes, 0, bytes.Length);

                writer.Write(Configuration.Network.Get().GetPublicKeyHash());
                writer.Write(publicKeyHash);

                return Encoders.Base58Check.EncodeData(stream.ToArray());
            }
        }

        public static string FromPrivateKey(Key privateKey)
        {
            return FromPublicKey(privateKey.PubKey);
        }

        public static bool Validate(string address)
        {
            return Encoders.Base58Check.DecodeData(address)[0] == Configuration.Network.Get().GetPublicKeyHash();
        }
    }
}
