using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetMaximum.ProtonLock.Exceptions;
using NetMaximum.ProtonLock.MediatR.Extensions;
using ServiceCollectionExtensions = NetMaximum.ProtonLock.MediatR.Extensions.ServiceCollectionExtensions;

namespace NetMaximum.ProtonLock.MediatR.UnitTests.Extensions;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void Given_a_container_without_mediatr_configured_when_protonlock_added_it_should_thrown_an_exception()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();

        // Act
        Action result = () => { serviceCollection.AddProtonLockWithMediatR(); };

        // Assert
        result.Should().Throw<InfrastructureException>().WithMessage("MediatR cannot be found");
    }
}