using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Redis;

namespace NetMaximum.ProtonLock.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddProtonLock(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddSingleton<IClient>(provider => new Client(provider.GetService<IConnectionMultiplexer>(), TimeSpan.FromSeconds(10)));
    }
}