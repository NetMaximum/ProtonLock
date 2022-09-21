namespace NetMaximum.ProtonLock.UnitTests;

public class SampleCommand : IFingerprint
{
    public string Id { get; init; } = string.Empty;
    
    public string FingerPrint()
    {
        return Id;
    }
}