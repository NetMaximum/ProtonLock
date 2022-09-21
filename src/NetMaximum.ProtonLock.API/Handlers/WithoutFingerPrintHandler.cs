using MediatR;
using NetMaximum.ProtonLock.API.Models;

namespace NetMaximum.ProtonLock.API.Handlers;

public class WithoutFingerPrintHandler : IRequestHandler<MediatRWithoutFingerPrint>
{
    public Task<Unit> Handle(MediatRWithoutFingerPrint request, CancellationToken cancellationToken)
    {
        return Unit.Task;
    }
}