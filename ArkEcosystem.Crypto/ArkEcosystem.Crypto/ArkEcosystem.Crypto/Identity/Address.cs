using NBitcoin.DataEncoders;
using NBitcoin;
using SshNet.Security.Cryptography;
using System.IO;

namespace ArkEcosystem.Crypto.Identity
{
    public static class Address
    {
        static readonly RIPEMD160 Ripemd160 = new RIPEMD160();

        public static string FromSecret(string secret, byte publicKeyHash = 0)
        {
            return FromPrivateKey(PrivateKey.FromSecret(secret), publicKeyHash);
        }

        public static string FromPublicKey(PubKey publicKey, byte publicKeyHash = 0)
        {
            MemoryStream stream = new MemoryStream();

            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                var bytes = publicKey.ToBytes();

                if (publicKeyHash != 0)
                {
                    writer.Write(publicKeyHash);
                } else {
                    writer.Write(Configuration.Network.Get().GetPublicKeyHash());
                }

                writer.Write(Ripemd160.ComputeHash(bytes, 0, bytes.Length));

                return Encoders.Base58Check.EncodeData(stream.ToArray());
            }
        }

        public static string FromPrivateKey(Key privateKey, byte publicKeyHash = 0)
        {
            return FromPublicKey(privateKey.PubKey, publicKeyHash);
        }

        public static bool Validate(string address, byte publicKeyHash = 0)
        {
            var addressPrefix = Encoders.Base58Check.DecodeData(address)[0];

            if (publicKeyHash != 0)
            {
                return addressPrefix == publicKeyHash;
            }
            else
            {
                return addressPrefix == Configuration.Network.Get().GetPublicKeyHash();
            }
        }
    }
}
