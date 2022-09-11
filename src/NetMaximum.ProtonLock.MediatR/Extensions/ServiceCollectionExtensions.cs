using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Redis;

namespace NetMaximum.ProtonLock.MediatR.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddProtonLockWithMediatR(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddSingleton<IClient>(provider => new Client(provider.GetService<IConnectionMultiplexer>(), TimeSpan.FromSeconds(10)));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ProtonLockBehaviour<,>));
    }
}