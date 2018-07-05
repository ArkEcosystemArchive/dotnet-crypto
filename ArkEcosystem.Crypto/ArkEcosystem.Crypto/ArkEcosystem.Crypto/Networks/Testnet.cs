using System;

namespace ArkEcosystem.Crypto.Networks
{
    public class Testnet : INetwork
    {
        public byte GetPublicKeyHash()
        {
            return 0x17;
        }

        public DateTime GetEpoch()
        {
            return new DateTime(2017, 3, 21, 13, 00, 0, DateTimeKind.Utc);
        }

        public string GetNethash()
        {
            return "d9acd04bde4234a81addb8482333b4ac906bed7be5a9970ce8ada428bd083192";
        }

        public byte GetWIF()
        {
            return 186;
        }
    }
}
