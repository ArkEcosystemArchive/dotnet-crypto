using NBitcoin.Crypto;
using NBitcoin.DataEncoders;
using NBitcoin;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ArkEcosystem.Crypto
{
    public class Message
    {
        static readonly System.Security.Cryptography.SHA256 Sha256 = System.Security.Cryptography.SHA256.Create();
        readonly string publicKey;
        readonly string signature;
        readonly string message;

        public Message(string publicKey, string signature, string message)
        {
            this.publicKey = publicKey;
            this.signature = signature;
            this.message = message;
        }

        public static Message Sign(string message, string secret)
        {
            var privateKeyBytes = Sha256.ComputeHash(Encoding.ASCII.GetBytes(secret));
            var privateKey = new Key(privateKeyBytes);

            var messageBytes = Sha256.ComputeHash(Encoding.ASCII.GetBytes(message));
            var signature = privateKey.Sign(new uint256(messageBytes));

            return new Message(
                privateKey.PubKey.ToString(),
                Encoders.Hex.EncodeData(signature.ToDER()),
                message
            );
        }

        public bool Verify()
        {
            var messageBytes = Sha256.ComputeHash(Encoding.ASCII.GetBytes(message));
            var messageSignature = ECDSASignature.FromDER(Encoders.Hex.DecodeData(signature));

            return new PubKey(publicKey).Verify(new uint256(messageBytes), messageSignature);
        }

        public string ToJson()
        {
            var json = new Dictionary<string, string>
            {
                { "publickey", publicKey },
                { "signature", signature },
                { "message", message }
            };

            return JObject.FromObject(json).ToString();
        }
    }
}
