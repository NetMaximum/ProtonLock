using FluentAssertions;
using NetArchTest.Rules;
using NetMaximum.ProtonLock.Exceptions;
using NetMaximum.ProtonLock.MediatR.Exceptions;
using Xunit.Abstractions;

namespace NetMaximum.ProtonLock.MediatR.UnitTests.Exceptions;

public class ExceptionTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ExceptionTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Given_a_series_of_exceptions_they_should_all_derive_from_protonbase()
    {
        
        // Arrange - Act
        var types = Types.InAssembly(typeof(InfrastructureException).Assembly)
            .That()
            .ResideInNamespace("NetMaximum.ProtonLock.MediatR.Exceptions")
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