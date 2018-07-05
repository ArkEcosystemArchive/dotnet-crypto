using System;

public interface INetwork
{
    byte GetPublicKeyHash();
    DateTime GetEpoch();
    string GetNethash();
    byte GetWIF();
}
