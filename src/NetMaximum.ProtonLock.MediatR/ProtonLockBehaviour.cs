using MediatR;

namespace NetMaximum.ProtonLock.MediatR;

public class ProtonLockBehaviour<TRequest, TResponse>  : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IClient _lockClient;

    public ProtonLockBehaviour(IClient lockClient)
    {
        _lockClient = lockClient;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var result = await _lockClient.DuplicateOccurenceAsync(request);
        return default;
    }
}