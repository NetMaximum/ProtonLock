using MediatR;
using NetMaximum.ProtonLock.Net6.API.Models;

namespace NetMaximum.ProtonLock.Net6.API.Handlers;

public class FingerPrintHandler : IRequestHandler<MediatRFingerPrint>
{
    public Task<Unit> Handle(MediatRFingerPrint request, CancellationToken cancellationToken)
    {
        return Unit.Task;
    }
}