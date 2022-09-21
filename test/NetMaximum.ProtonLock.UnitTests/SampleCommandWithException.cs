using System;

namespace NetMaximum.ProtonLock.UnitTests;

public class SampleCommandWithException : IFingerprint
{
    public string Id { get; init; } = string.Empty;
    
    public string FingerPrint()
    {
        throw new NotImplementedException();
    }
}