using NBitcoin;
using NBitcoin.DataEncoders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ArkEcosystem.Crypto
{
    public class TransactionModel
    {
        static readonly System.Security.Cryptography.SHA256 Sha256 = System.Security.Cryptography.SHA256.Create();

        public byte Header { get; set; }
        public byte Network { get; set; }
        public byte Type { get; set; }
        public byte Version { get; set; }
        public Dictionary<string, dynamic> Asset = new Dictionary<string, dynamic>();
        public int TimelockType { get; set; }
        public List<string> Signatures { get; set; }
        public string Id { get; set; }
        public string RecipientId { get; set; }
        public string SecondSignature { get; set; }
        public string SenderPublicKey { get; set; }
        public string Signature { get; set; }
        public string SignSignature { get; set; }
        public string VendorField { get; set; }
        public string VendorFieldHex { get; set; }
        public uint Expiration { get; set; }
        public uint Timestamp { get; set; }
        public ulong Amount { get; set; }
        public ulong Fee { get; set; }
        public ulong Timelock { get; set; }

        public string GetId()
        {
            return Encoders.Hex.EncodeData(Sha256.ComputeHash(ToBytes(false, false)));
        }

        public byte[] ToBytes(bool skipSignature = true, bool skipSecondSignature = true)
        {
            MemoryStream stream = new MemoryStream();

            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(Type);
                writer.Write(Timestamp);
                writer.Write(Encoders.Hex.DecodeData(SenderPublicKey));

                if (RecipientId != null)
                {
                    writer.Write(Encoders.Base58Check.DecodeData(RecipientId));
                }
                else
                {
                    writer.Write(new byte[21]);
                }

                if (VendorField != null)
                {
                    var vendorFieldBytes = Encoding.ASCII.GetBytes(VendorField);

                    if (vendorFieldBytes.Length < 65)
                    {
                        writer.Write(vendorFieldBytes);
                        writer.Write(new byte[64 - vendorFieldBytes.Length]);
                    }
                }
                else
                {
                    writer.Write(new byte[64]);
                }

                writer.Write(Amount);
                writer.Write(Fee);

                if (Type == Enums.TransactionTypes.SECOND_SIGNATURE_REGISTRATION)
                {
                    writer.Write(Encoders.Hex.DecodeData(Asset["signature"]["publicKey"]));
                }

                if (Type == Enums.TransactionTypes.DELEGATE_REGISTRATION)
                {
                    writer.Write(Encoding.ASCII.GetBytes(Asset["delegate"]["username"]));
                }

                if (Type == Enums.TransactionTypes.VOTE)
                {
                    writer.Write(Encoding.ASCII.GetBytes(string.Join("", Asset["votes"])));
                }

                if (Type == Enums.TransactionTypes.MULTI_SIGNATURE_REGISTRATION)
                {
                    writer.Write((byte)Asset["multisignature"]["min"]);
                    writer.Write((byte)Asset["multisignature"]["lifetime"]);
                    writer.Write(Encoding.ASCII.GetBytes(string.Join("", Asset["multisignature"]["keysgroup"])));
                }

                if (!skipSignature && Signature != null)
                {
                    writer.Write(Encoders.Hex.DecodeData(Signature));
                }

                if (!skipSecondSignature && SignSignature != null)
                {
                    writer.Write(Encoders.Hex.DecodeData(SignSignature));
                }

                return stream.ToArray();
            }
        }

        public string Sign(string secret)
        {
            SenderPublicKey = Encoders.Hex.EncodeData(Identity.PublicKey.FromSecret(secret).ToBytes());

            var signature = Identity.PrivateKey
                .FromSecret(secret)
                .Sign(new uint256(Sha256.ComputeHash(ToBytes())));

            return Encoders.Hex.EncodeData(signature.ToDER());
        }

        public string SecondSign(string secret)
        {
            var signature = Identity.PrivateKey
                .FromSecret(secret)
                .Sign(new uint256(Sha256.ComputeHash(ToBytes(true))));

            return Encoders.Hex.EncodeData(signature.ToDER());
        }

        public bool Verify()
        {
            var signature = Encoders.Hex.DecodeData(Signature);
            var transactionBytes = ToBytes();

            return Identity.PublicKey
                .FromString(SenderPublicKey)
                .Verify(new uint256(Sha256.ComputeHash(transactionBytes)), signature);
        }

        public bool SecondVerify(string secondPublicKey)
        {
            var signature = Encoders.Hex.DecodeData(SignSignature);
            var transactionBytes = ToBytes(false);

            return Identity.PublicKey
                .FromString(secondPublicKey)
                .Verify(new uint256(Sha256.ComputeHash(transactionBytes)), signature);
        }

        public TransactionModel ParseSignatures(string serialised, int startOffset)
        {
            Signature = serialised.Substring(startOffset);

            var multiSignatureOffset = 0;

            if (Signature.Length == 0)
            {
                Signature = null;
            }
            else
            {
                var signatureLength = Convert.ToByte(Signature.Substring(2, 2), 16) + 2;
                Signature = serialised.Substring(startOffset, signatureLength * 2);
                multiSignatureOffset += signatureLength * 2;
                SecondSignature = serialised.Substring(startOffset + signatureLength * 2);

                if (SecondSignature.Length == 0)
                {
                    SecondSignature = null;
                }
                else
                {
                    if (SecondSignature.Substring(0, 2) == "ff")
                    {
                        SecondSignature = null;
                    }
                    else
                    {
                        var secondSignatureLength = Convert.ToByte(SecondSignature.Substring(2, 2), 16) + 2;
                        SecondSignature = SecondSignature.Substring(0, secondSignatureLength * 2);
                        multiSignatureOffset += secondSignatureLength * 2;
                    }
                }

                var signatures = serialised.Substring(startOffset + multiSignatureOffset);

                if (signatures.Length == 0)
                {
                    return this;
                }

                if (signatures.Substring(0, 2) != "ff")
                {
                    return this;
                }

                signatures = signatures.Substring(2);
                List<string> signaturesList = new List<string>();

                while (true)
                {
                    if (signatures == "")
                    {
                        break;
                    }

                    var multiSignatureLength = Convert.ToByte(signatures.Substring(2, 2), 16) + 2;

                    if (multiSignatureLength > 0)
                    {
                        signaturesList.Add(signatures.Substring(0, multiSignatureLength * 2));
                    }

                    signatures = signatures.Substring(multiSignatureLength * 2);
                }

                Signatures = signaturesList;
            }

            return this;
        }
    }
}
