using System.Net;
using System.Net.Http.Json;

namespace NetMaximum.ProtonLock.API.IntegrationTests;

public class LockControllerTests : IClassFixture<ApiFactory>
{
    private readonly ApiFactory _apiFactory;
    private readonly HttpClient _httpClient;

    public LockControllerTests(ApiFactory apiFactory)
    {
        _apiFactory = apiFactory;
        _httpClient = apiFactory.CreateClient();
    }
    
    
    [Fact]
    public async Task Given_a_fingerprint_when_a_object_is_seen_again_within_a_timeframe_then_a_conflict_should_be_returned()
    {
        // Arrange 
        
        // Act 
        var response = await _httpClient.PostAsJsonAsync("/lock/with-fingerprint", new
        {
            FirstName = "Chriss"
        });
        
        // Assert
        Assert.True(response.StatusCode == HttpStatusCode.Conflict);
    }
}