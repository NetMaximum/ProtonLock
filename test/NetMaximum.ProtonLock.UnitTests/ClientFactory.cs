using System.Threading.Tasks;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using StackExchange.Redis;
using Xunit;

namespace NetMaximum.ProtonLock.UnitTests;

public class ClientFactory : IAsyncLifetime
{
    private TestcontainerDatabase _redis = new TestcontainersBuilder<RedisTestcontainer>()
        .WithDatabase(new RedisTestcontainerConfiguration())
        .Build();

    public IConnectionMultiplexer RedisConnection { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        ConfigurationOptions configuration = new ConfigurationOptions
        {
            AbortOnConnectFail = true,
            ConnectTimeout = 5000,
            User = "default",
            Password = "redispw"
        };

        await _redis.StartAsync();
        
        
        configuration.EndPoints.Add(_redis.ConnectionString);
        RedisConnection = await ConnectionMultiplexer.ConnectAsync(configuration);
    }

    public async Task DisposeAsync()
    {
        await _redis.StopAsync();
    }
}