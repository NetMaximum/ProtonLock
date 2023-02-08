using MediatR;
using NetMaximum.ProtonLock.Net7.Api.Models;

namespace NetMaximum.ProtonLock.Net7.Api.Handlers;

public class FingerPrintHandler : IRequestHandler<MediatRFingerPrint>
{
    public Task<Unit> Handle(MediatRFingerPrint request, CancellationToken cancellationToken)
    {
        return Unit.Task;
    }
}