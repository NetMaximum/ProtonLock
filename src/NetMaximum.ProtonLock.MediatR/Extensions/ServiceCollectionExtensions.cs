using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NetMaximum.ProtonLock.Exceptions;
using StackExchange.Redis;

namespace NetMaximum.ProtonLock.MediatR.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddProtonLockWithMediatR(this IServiceCollection serviceCollection, Action<ProtonLockOptions>? options = null, IConfiguration? namedSection = null)
    {
        var myOptions = new ProtonLockOptions();
        namedSection?.Bind(myOptions);
        options?.Invoke(myOptions);

        // Checks that mediatr has been configured.
        var result = serviceCollection.Any(x => x.ServiceType == typeof(IMediator));
        if (result == false)
        {
            throw new InfrastructureException("MediatR cannot be found");
        }
        
        serviceCollection.TryAddSingleton<IClient>(provider => new Client(provider.GetService<IConnectionMultiplexer>()!, TimeSpan.FromMilliseconds(myOptions.TimeOut)));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ProtonLockBehaviour<,>));
    }
}