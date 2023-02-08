using System.Net;
using System.Net.Http.Json;
using FluentAssertions;

namespace NetMaximum.ProtonLock.API.IntegrationTests;

public class LockControllerTests : IClassFixture<ApiFactory>
{
    private readonly HttpClient _httpClient;

    public LockControllerTests(ApiFactory apiFactory)
    {
        _httpClient = apiFactory.CreateClient();
    }

    [Fact]
    public async Task Given_a_object_that_doesnt_implement_fingerprinting_then_it_should_always_be_accepted()
    {
        // Arrange
        
        // Act
        var response = await _httpClient.PostAsJsonAsync("/lock/without-fingerprint", new
        {
            Name = "Chriss"
        });

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        // Assert

    }
    
    [Fact]
    public async Task Given_a_fingerprint_when_a_object_is_seen_again_within_a_timeframe_then_a_conflict_should_be_returned()
    {
        // Arrange 
        var response = await _httpClient.PostAsJsonAsync("/lock/with-fingerprint", new
        {
            Name = "Chriss"
        });
        
        // Act 
        var locked = await _httpClient.PostAsJsonAsync("/lock/with-fingerprint", new
        {
            Name = "Chriss"
        });
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        locked.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }
}