using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit.Abstractions;

namespace NetMaximum.ProtonLock.API.IntegrationTests;

public class MediatRControllerTests : IClassFixture<ApiFactory>
{
    private readonly ApiFactory _apiFactory;
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly HttpClient _httpClient;

    public MediatRControllerTests(ApiFactory apiFactory, ITestOutputHelper testOutputHelper)
    {
        _apiFactory = apiFactory;
        _testOutputHelper = testOutputHelper;
        _httpClient = apiFactory.CreateClient();
    }

    [Fact]
    public async Task Given_a_object_that_doesnt_implement_fingerprinting_then_it_should_always_be_accepted()
    {
        // Arrange
        
        // Act
        var response = await _httpClient.PostAsJsonAsync("/MediatR/without-fingerprint", new
        {
            Name = "Chriss"
        });

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        // Assert

    }
    
    [Fact]
    public async Task Given_a_fingerprint_when_a_object_is_seen_again_within_a_timeframe_then_a_conflict_should_be_returned()
    {
        var id = Guid.NewGuid().ToString();
        
        // Arrange 
        var response = await _httpClient.PostAsJsonAsync("/MediatR/with-fingerprint", new
        {
            Name = id
        });
        
        // Act 
        var locked = await _httpClient.PostAsJsonAsync("/MediatR/with-fingerprint", new
        {
            Name = id
        });
        
        // Assert
        var resp = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(resp);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        locked.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }
}