using System;

namespace Netmaximum.ProtonLock.UnitTests;

public class SampleCommand : IFingerprint
{
    public string Id { get; init; }
    
    public string FingerPrint()
    {
        return Id;
    }
}