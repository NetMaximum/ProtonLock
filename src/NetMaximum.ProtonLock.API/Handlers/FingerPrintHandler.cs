using MediatR;
using NetMaximum.ProtonLock.API.Models;

namespace NetMaximum.ProtonLock.API.Handlers;

public class FingerPrintHandler : IRequestHandler<MediatRFingerPrint>
{
    public Task<Unit> Handle(MediatRFingerPrint request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}