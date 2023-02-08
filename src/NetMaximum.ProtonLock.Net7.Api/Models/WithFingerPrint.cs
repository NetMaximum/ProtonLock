namespace NetMaximum.ProtonLock.Net7.Api.Models;

public class WithFingerPrint : WithoutFingerPrint, IFingerprint
{
    public Requirement FingerPrint()
    {
        return new Requirement(Name);
    }
}