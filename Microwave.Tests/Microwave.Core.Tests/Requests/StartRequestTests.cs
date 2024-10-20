using Microwave.Api.Validators.Requests;
using Microwave.Core.Requests.Execution;

namespace Microwave.Tests.Microwave.Core.Tests.Requests;

public class StartRequestTests
{
    private readonly StartRequestValidator _validator = new();

    [Fact]
    public void Should_Be_Valid_When_All_Is_Ok()
    {
        var startRequest = new StartRequest() { Power = 5, Seconds = 100, PredefinedProgramId = null };

        var validationResult = _validator.Validate(startRequest);

        Assert.True(validationResult.IsValid);
    }
    [Fact]
    public void Should_Be_Valid_When_All_Is_Ok_And_With_ProgramId()
    {
        var startRequest = new StartRequest() { Power = 5, Seconds = 100, PredefinedProgramId = Guid.NewGuid() };

        var validationResult = _validator.Validate(startRequest);

        Assert.True(validationResult.IsValid);
    }
    [Fact]
    public void Should_Be_Valid_When_Using_Default_Values()
    {
        var startRequest = new StartRequest();

        var validationResult = _validator.Validate(startRequest);

        Assert.True(validationResult.IsValid);
    }
    [Fact]
    public void Should_Be_Valid_When_Without_Seconds_And_Power_But_With_ProgramId()
    {
        var startRequest = new StartRequest() { Seconds = 0, Power = 0, PredefinedProgramId = Guid.NewGuid() };

        var validationResult = _validator.Validate(startRequest);

        Assert.True(validationResult.IsValid);
    }
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void Should_Be_Invalid_When_Power_Is_Less_Than_One(int power)
    {
        var startRequest = new StartRequest() { Power = power, Seconds = 30, PredefinedProgramId = null };

        var validationResult = _validator.Validate(startRequest);

        Assert.True(!validationResult.IsValid);
    }
    [Theory]
    [InlineData(11)]
    [InlineData(150)]
    [InlineData(int.MaxValue)]
    public void Should_Be_Invalid_When_Power_Is_Over_Ten(int power)
    {
        var startRequest = new StartRequest() { Power = power, Seconds = 30, PredefinedProgramId = null };

        var validationResult = _validator.Validate(startRequest);

        Assert.True(!validationResult.IsValid);
    }
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void Should_Be_Invalid_When_Seconds_Is_Less_Than_One(int seconds)
    {
        var startRequest = new StartRequest() { Seconds = seconds };

        var validationResult = _validator.Validate(startRequest);

        Assert.True(!validationResult.IsValid);
    }
    [Theory]
    [InlineData(121)]
    [InlineData(150)]
    [InlineData(int.MaxValue)]
    public void Should_Be_Invalid_When_Seconds_Is_Over_120(int seconds)
    {
        var startRequest = new StartRequest() { Seconds = seconds };

        var validationResult = _validator.Validate(startRequest);

        Assert.True(!validationResult.IsValid);
    }
}
