using System;

namespace ArkEcosystem.Crypto
{
    public static class Slot
    {
        public static uint GetTime()
        {
            return Convert.ToUInt32((DateTime.UtcNow - Configuration.Network.Get().GetEpoch()).TotalMilliseconds / 1000);
        }
    }
}
