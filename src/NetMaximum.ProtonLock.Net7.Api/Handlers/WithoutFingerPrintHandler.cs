using MediatR;
using NetMaximum.ProtonLock.Net7.Api.Models;

namespace NetMaximum.ProtonLock.Net7.Api.Handlers;

public class WithoutFingerPrintHandler : IRequestHandler<MediatRWithoutFingerPrint>
{
    public Task<Unit> Handle(MediatRWithoutFingerPrint request, CancellationToken cancellationToken)
    {
        return Unit.Task;
    }
}