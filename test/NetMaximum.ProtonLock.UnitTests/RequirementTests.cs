using System;
using FluentAssertions;
using Xunit;

namespace NetMaximum.ProtonLock.UnitTests;

public class RequirementTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Given_a_requirement_when_it_doesnt_have_a_key_then_an_exception_is_thrown(string key)
    {
        // Arrange - Act
        Action result = () =>
        {
            var req = new Requirement(key);
        };

        // Assert
        result.Should().Throw<Exception>();
    } 
}