using System.Text;
using System.IO;

namespace ArkEcosystem.Crypto.Serialisers
{
    public static class DelegateRegistration
    {
        public static void Serialise(BinaryWriter writer, TransactionModel transaction)
        {
            var delegateBytes = Encoding.ASCII.GetBytes(transaction.Asset["delegate"]["username"]);

            writer.Write((byte)delegateBytes.Length);
            writer.Write(delegateBytes);
        }
    }
}
