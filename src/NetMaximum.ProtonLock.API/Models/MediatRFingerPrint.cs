using MediatR;

namespace NetMaximum.ProtonLock.API.Models;

public class MediatRFingerPrint : IFingerprint, IRequest<Unit>
{
    public string Name { get; set; } = string.Empty;
    public int age { get; set; } = 10;
    
    public string FingerPrint()
    {
        
        return Name + age;
    }
    // Async Task for allowing other things....
}
