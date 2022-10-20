namespace NetMaximum.ProtonLock.API.Models;

public class WithFingerPrint : WithoutFingerPrint, IFingerprint
{
    public Requirement FingerPrint()
    {
        return new Requirement(Name);
    }
}