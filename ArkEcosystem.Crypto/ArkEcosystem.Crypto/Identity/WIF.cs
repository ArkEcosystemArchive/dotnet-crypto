using NBitcoin.DataEncoders;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace ArkEcosystem.Crypto.Identity
{
    public static class WIF
    {
        static readonly SHA256 Sha256 = SHA256.Create();

        public static string FromSecret(string secret)
        {
            MemoryStream stream = new MemoryStream();

            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(Configuration.Network.Get().GetWIF());
                writer.Write(Sha256.ComputeHash(Encoding.ASCII.GetBytes(secret)));
                writer.Write((byte)0x01);

                return Encoders.Base58Check.EncodeData(stream.ToArray());
            }
        }
    }
}
