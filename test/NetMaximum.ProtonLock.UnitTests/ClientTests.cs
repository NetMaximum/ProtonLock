using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace NetMaximum.ProtonLock.UnitTests;

public class ClientTests : IClassFixture<ClientFactory>
{
    private readonly ClientFactory _clientFactory;

    public ClientTests(ClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }
    
    [Fact]
    public async Task Given_an_fingerprint_when_it_not_been_seen_then_duplicate_occurence_is_false()
    {
        // Arrange
        var subject = new Client(_clientFactory.RedisConnection, TimeSpan.FromSeconds(10));
        var fingerprint = new SampleCommand
        {
            Id = Guid.NewGuid().ToString()
        };
        
        // Act
        var result = await  subject.DuplicateOccurenceAsync(fingerprint);
        
        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task Given_an_fingerprint_when_its_been_seen_within_a_timespan_then_duplicate_occurrence_is_true()
    {
        // Arrange
        var subject = new Client(_clientFactory.RedisConnection, TimeSpan.FromSeconds(10));
        var fingerprint = new SampleCommand
        {
            Id = Guid.NewGuid().ToString()
        };
        
        // Act
        var result = await subject.DuplicateOccurenceAsync(fingerprint);
        var duplicateResult = await subject.DuplicateOccurenceAsync(fingerprint);
        
        // Assert
        result.Should().BeFalse();
        duplicateResult.Should().BeTrue();
    }

    [Fact]
    public async Task Given_an_fingerprint_that_has_been_seen_within_a_timespan_when_the_timespan_is_elapsed_then_duplicate_occurrence_is_false()
    {
        // Arrange
        var subject = new Client(_clientFactory.RedisConnection, TimeSpan.FromMilliseconds(100));
        var fingerprint = new SampleCommand
        {
            Id = Guid.NewGuid().ToString()
        };
        
        // Act
        var result = await subject.DuplicateOccurenceAsync(fingerprint);
        await Task.Delay(500);
        var duplicateResult = await subject.DuplicateOccurenceAsync(fingerprint);
        
        // Assert
        result.Should().BeFalse();
        duplicateResult.Should().BeFalse();
    }

    [Fact]
    public async Task Given_an_exception_inside_redis_when_thrown_then_duplicate_occurrence_then_an_exception_is_thrown()
    {
        // Arrange
        var subject = new Client(_clientFactory.RedisConnection, TimeSpan.FromSeconds(10));
        var fingerprint = new SampleCommandWithException();
        
        // Act
        var result =  async () => await subject.DuplicateOccurenceAsync(fingerprint);

        // Assert
        await result.Should().ThrowAsync<Exception>().WithMessage("The method or operation is not implemented.");
    }
}