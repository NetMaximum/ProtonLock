using NetMaximum.ProtonLock.Exceptions;

namespace NetMaximum.ProtonLock;

public class Requirement
{
    /// <summary>
    /// A unique string that identifies the object.
    /// </summary>
    public string Fingerprint { get; }
    
    /// <summary>
    /// If null it's defaulted to the elapsed timespan set in the configuration.
    /// </summary>
    public TimeSpan? Duration { get; }

    public Requirement(string fingerprint, TimeSpan duration)
    {
        if (string.IsNullOrEmpty(fingerprint))
        {
            throw new KeyException("The fingerprint is null or empty");
        }
        
        Fingerprint = fingerprint;

        if (duration != TimeSpan.Zero)
        {
            Duration = duration;    
        }
    }
    
    public Requirement(string fingerprint) : this(fingerprint, TimeSpan.Zero)
    {
    }
}