using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Redis;

namespace NetMaximum.ProtonLock.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddProtonLock(this IServiceCollection serviceCollection, Action<ProtonLockOptions>? options = null, IConfiguration? namedSection = null)
    {
        var myOptions = new ProtonLockOptions();
        namedSection?.Bind(myOptions);
        options?.Invoke(myOptions);
        
        serviceCollection.TryAddSingleton<IClient>(provider => new Client(provider.GetService<IConnectionMultiplexer>()!, TimeSpan.FromMilliseconds(myOptions.TimeOut)));
    }
}