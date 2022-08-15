using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Netmaximum.ProtonLock.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddProtonLock(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddSingleton<Client,Client>();
    }
}