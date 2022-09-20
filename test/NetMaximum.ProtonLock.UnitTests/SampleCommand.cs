using System;

namespace NetMaximum.ProtonLock.UnitTests;

public class SampleCommand : IFingerprint
{
    public string Id { get; init; }
    
    public string FingerPrint()
    {
        return Id;
    }
}