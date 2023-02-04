using MediatR;

namespace NetMaximum.ProtonLock.Net7.Api.Models;

public class MediatRFingerPrint : IFingerprint, IRequest<Unit>
{
    public string Name { get; set; } = string.Empty;
    public int age { get; set; } = 10;
    
    public Requirement FingerPrint()
    {
        return new Requirement(Name + age);
    }
}
