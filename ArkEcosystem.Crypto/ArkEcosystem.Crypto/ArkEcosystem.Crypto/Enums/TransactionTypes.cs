namespace ArkEcosystem.Crypto.Enums
{
    public static class TransactionTypes
    {
        public static readonly byte TRANSFER = 0;
        public static readonly byte SECOND_SIGNATURE_REGISTRATION = 1;
        public static readonly byte DELEGATE_REGISTRATION = 2;
        public static readonly byte VOTE = 3;
        public static readonly byte MULTI_SIGNATURE_REGISTRATION = 4;
        public static readonly byte IPFS = 5;
        public static readonly byte TIMELOCK_TRANSFER = 6;
        public static readonly byte MULTI_PAYMENT = 7;
        public static readonly byte DELEGATE_RESIGNATION = 8;
    }
}
