using System.Collections.Generic;
using System.IO;

namespace ArkEcosystem.Crypto.Deserialisers
{
    public static class DelegateRegistration
    {
        public static TransactionModel Deserialise(
            BinaryReader reader,
            MemoryStream stream,
            TransactionModel transaction,
            string serialised,
            int assetOffset
        )
        {
            stream.Position = assetOffset / 2;

            var usernameLength = reader.ReadByte() & 0xff;
            var username = serialised.Substring((assetOffset / 2 + 1) * 2, usernameLength * 2);

            transaction.Asset.Add("delegate", new Dictionary<string, string>());
            transaction.Asset["delegate"].Add("username", ConvertHexToString(username));

            return transaction.ParseSignatures(serialised, assetOffset + (usernameLength + 1) * 2);
        }

        private static string ConvertHexToString(string HexValue)
        {
            string StrValue = "";

            while (HexValue.Length > 0)
            {
                StrValue += System.Convert.ToChar(System.Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
                HexValue = HexValue.Substring(2, HexValue.Length - 2);
            }

            return StrValue;
        }
    }
}
