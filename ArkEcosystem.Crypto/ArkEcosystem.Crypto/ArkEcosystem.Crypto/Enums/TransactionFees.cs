using System;

namespace ArkEcosystem.Crypto.Enums
{
    public static class TransactionFees
    {
        public static readonly UInt64 TRANSFER                      = 10000000;
        public static readonly UInt64 SECOND_SIGNATURE_REGISTRATION = 500000000;
        public static readonly UInt64 DELEGATE_REGISTRATION         = 2500000000;
        public static readonly UInt64 VOTE                          = 100000000;
        public static readonly UInt64 MULTI_SIGNATURE_REGISTRATION  = 500000000;
        public static readonly UInt64 IPFS                          = 0;
        public static readonly UInt64 TIMELOCK_TRANSFER             = 0;
        public static readonly UInt64 MULTI_PAYMENT                 = 0;
        public static readonly UInt64 DELEGATE_RESIGNATION          = 0;
    }
}
