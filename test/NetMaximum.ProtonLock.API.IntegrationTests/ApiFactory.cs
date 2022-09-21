using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Redis;

namespace NetMaximum.ProtonLock.API.IntegrationTests;

public class ApiFactory : WebApplicationFactory<IMarker>, IAsyncLifetime
{
    private TestcontainerDatabase _redis = new TestcontainersBuilder<RedisTestcontainer>()
        .WithDatabase(new RedisTestcontainerConfiguration())
        .Build();

    private IConnectionMultiplexer? _connectionMultiplexer;
    
    public async Task InitializeAsync()
    {
        await _redis.StartAsync();
        _connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(_redis.ConnectionString);
    }


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(collection =>
        {
            collection.RemoveAll(typeof(IConnectionMultiplexer));
            collection.AddSingleton(_connectionMultiplexer!);
        });
    }

    public new async Task DisposeAsync()
    {
        await _redis.StopAsync();
    }
}