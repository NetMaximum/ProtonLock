using MediatR;

namespace NetMaximum.ProtonLock.Net7.Api.Models;

public class MediatRWithoutFingerPrint : IRequest<Unit>
{
    public string Name { get; set; } = string.Empty;
    
    public string FingerPrint()
    {
        return Name;
    }
}