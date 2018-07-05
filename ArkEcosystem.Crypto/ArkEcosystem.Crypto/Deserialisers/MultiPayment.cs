using NBitcoin.DataEncoders;
using System.Collections.Generic;
using System.IO;

namespace ArkEcosystem.Crypto.Deserialisers
{
    public static class MultiPayment
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

            var total = reader.ReadByte() & 0xff;
            var offset = assetOffset / 2 + 1;

            transaction.Asset.Add("payments", new List<Dictionary<string, dynamic>>());
            for (int i = 0; i < total; i++)
            {
                stream.Position += offset;

                Dictionary<string, dynamic> payment = new Dictionary<string, dynamic>();
                payment["amount"] = reader.ReadUInt64();
                payment["recipientId"] = Encoders.Base58Check.EncodeData(reader.ReadBytes(21));

                transaction.Asset["payments"].Add(payment);

                offset += 22;
            }

            foreach (var payment in transaction.Asset["payments"])
            {
                transaction.Amount += payment["amount"];
            }

            return transaction.ParseSignatures(serialised, offset * 2);
        }
    }
}
