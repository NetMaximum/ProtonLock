using Netmaximum.ProtonLock;

namespace NetMaximum.ProtonLock.API.Models;

public class WithFingerPrint : WithoutFingerPrint, IFingerprint
{
    public string FingerPrint()
    {
        return Name;
    }
}