using System;

namespace NetMaximum.ProtonLock.UnitTests;

public class SampleCommandWithException : IFingerprint
{
    public string Id { get; init; } = string.Empty;

    public Requirement FingerPrint()
    {
        throw new NotImplementedException();
    }
}