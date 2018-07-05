using System;

namespace ArkEcosystem.Crypto.Networks
{
    public class Devnet : INetwork
    {
        public byte GetPublicKeyHash()
        {
            return 0x1e;
        }

        public DateTime GetEpoch()
        {
            return new DateTime(2017, 3, 21, 13, 00, 0, DateTimeKind.Utc);
        }

        public string GetNethash()
        {
            return "578e820911f24e039733b45e4882b73e301f813a0d2c31330dafda84534ffa23";
        }

        public byte GetWIF()
        {
            return 170;
        }
    }
}
