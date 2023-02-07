using MediatR;
using NetMaximum.ProtonLock.Net6.API.Models;

namespace NetMaximum.ProtonLock.Net6.API.Handlers;

public class WithoutFingerPrintHandler : IRequestHandler<MediatRWithoutFingerPrint>
{
    public Task<Unit> Handle(MediatRWithoutFingerPrint request, CancellationToken cancellationToken)
    {
        return Unit.Task;
    }
}