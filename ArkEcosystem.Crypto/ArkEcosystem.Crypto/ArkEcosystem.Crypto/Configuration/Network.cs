namespace ArkEcosystem.Crypto.Configuration
{
    public static class Network
    {
        public static INetwork activeNetwork;

        public static INetwork Get()
        {
            if (activeNetwork == null)
            {
                activeNetwork = new Networks.Devnet();
            }

            return activeNetwork;
        }

        public static void Set(INetwork network)
        {
            activeNetwork = network;
        }
    }
}
