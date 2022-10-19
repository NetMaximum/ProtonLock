using System;
using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;
using NetMaximum.ProtonLock.Exceptions;
using Xunit;
using Xunit.Abstractions;

namespace NetMaximum.ProtonLock.UnitTests.Exceptions;

public class ExceptionTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ExceptionTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public void Given_a_series_of_exceptions_when_implemented_then_should_all_derive_from_protonbase()
    {
        // Arrange - Act
        var types = Types.InAssembly(typeof(ProtonLockException).Assembly)
            .That()
            .ResideInNamespace("NetMaximum.ProtonLock.Exceptions")
            .And().DoNotHaveName(nameof(ProtonLockException))
            .Should()
            .Inherit(typeof(ProtonLockException))
            .GetResult();

        // Assert
       
        if (types.FailingTypes != null)
        {
            _testOutputHelper.WriteLine($"Failing Types:");
            foreach (var typesFailingType in types.FailingTypes)
            {
                _testOutputHelper.WriteLine(typesFailingType.ToString());
            }
        }
        
        types.IsSuccessful.Should().BeTrue();
    }
}