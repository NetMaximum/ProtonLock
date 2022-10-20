using System;

namespace NetMaximum.ProtonLock.UnitTests;

public class SampleCommand : IFingerprint
{
    public string Id { get; init; } = string.Empty;
    public Requirement FingerPrint()
    {
        return new Requirement(Id);
    }
}

public class SampleCommandWithDuration : IFingerprint
{
    public string Id { get; init; } = string.Empty;
    
    public TimeSpan Duration { get; init; }
    
    public Requirement FingerPrint()
    {
        return new Requirement(Id, Duration);
    }
}