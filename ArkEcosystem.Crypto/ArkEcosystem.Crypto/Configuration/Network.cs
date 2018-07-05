namespace ArkEcosystem.Crypto.Configuration
{
    public static class Network
    {
        public static INetwork activeNetwork;

        public static INetwork Get()
        {
            return activeNetwork;
        }

        public static void Set(INetwork network)
        {
            activeNetwork = network;
        }
    }
}
