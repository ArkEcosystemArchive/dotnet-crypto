using System;

namespace ArkEcosystem.Crypto.Networks
{
    public class Mainnet : INetwork
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
            return "6e84d08bd299ed97c212c886c98a57e36545c8f5d645ca7eeae63a8bd62d8988";
        }

        public byte GetWIF()
        {
            return 170;
        }
    }
}
