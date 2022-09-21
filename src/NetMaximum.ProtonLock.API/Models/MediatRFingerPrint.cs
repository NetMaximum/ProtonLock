using MediatR;

namespace NetMaximum.ProtonLock.API.Models;

public class MediatRFingerPrint : IFingerprint, IRequest<Unit>
{
    public string Name { get; set; } = string.Empty;
    
    public string FingerPrint()
    {
        return Name;
    }
}