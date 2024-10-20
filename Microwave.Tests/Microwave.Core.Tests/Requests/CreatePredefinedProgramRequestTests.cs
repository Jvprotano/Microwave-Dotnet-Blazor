using Microwave.Api.Validators.Requests;
using Microwave.Core.Requests.PredefinedPrograms;

namespace Microwave.Tests.Microwave.Core.Tests.Requests;

public class CreatePredefinedProgramRequestTests
{
    private readonly CreatePredefinedProgramValidator _validator = new();
    private readonly string _name = "Valid Name";
    private readonly string _food = "Valid Food";
    private readonly string _instructions = "These are the instructions";
    private readonly string _labelHeating = "J";

    [Fact]
    public void Should_Be_Valid_When_All_Is_Ok()
    {
        var request = new CreatePredefinedProgramRequest(_name, _food, 10, 10, _labelHeating, _instructions);

        var validationResult = _validator.Validate(request);

        Assert.True(validationResult.IsValid);
    }
    [Fact]
    public void Should_Be_Valid_Instructions_Is_Null()
    {
        var request = new CreatePredefinedProgramRequest(_name, _food, 10, 10, _labelHeating, instructions: null);

        var validationResult = _validator.Validate(request);

        Assert.True(validationResult.IsValid);
    }
    [Fact]
    public void Should_Be_Valid_Instructions_Is_Empty()
    {
        var request = new CreatePredefinedProgramRequest(_name, _food, 10, 10, _labelHeating, instructions: "");

        var validationResult = _validator.Validate(request);

        Assert.True(validationResult.IsValid);
    }
    [Fact]
    public void Should_Be_Invalid_When_Name_Is_Empty()
    {
        var request = new CreatePredefinedProgramRequest("", _food, 10, 10, _labelHeating, instructions: "");

        var validationResult = _validator.Validate(request);

        Assert.False(validationResult.IsValid);
    }
    [Fact]
    public void Should_Be_Invalid_When_Name_Is_Null()
    {
        var request = new CreatePredefinedProgramRequest(null!, _food, 10, 10, _labelHeating, instructions: "");

        var validationResult = _validator.Validate(request);

        Assert.False(validationResult.IsValid);
    }
    [Fact]
    public void Should_Be_Invalid_When_Food_Is_Empty()
    {
        var request = new CreatePredefinedProgramRequest(_name, "", 10, 10, _labelHeating, instructions: "");

        var validationResult = _validator.Validate(request);

        Assert.False(validationResult.IsValid);
    }
    [Fact]
    public void Should_Be_Invalid_When_Food_Is_Null()
    {
        var request = new CreatePredefinedProgramRequest(_name, null!, 10, 10, _labelHeating, instructions: "");

        var validationResult = _validator.Validate(request);

        Assert.False(validationResult.IsValid);
    }
    [Fact]
    public void Should_Be_Invalid_When_LabelHeating_Is_Empty()
    {
        var request = new CreatePredefinedProgramRequest(_name, _food, 10, 10, "", instructions: "");

        var validationResult = _validator.Validate(request);

        Assert.False(validationResult.IsValid);
    }
    [Fact]
    public void Should_Be_Invalid_When_LabelHeating_Is_Null()
    {
        var request = new CreatePredefinedProgramRequest(_name, _food, 10, 10, null!, instructions: "");

        var validationResult = _validator.Validate(request);

        Assert.False(validationResult.IsValid);
    }
    [Fact]
    public void Should_Be_Invalid_When_LabelHeating_Is_Over_One_Length()
    {
        var request = new CreatePredefinedProgramRequest(_name, _food, 10, 10, "JJ", instructions: "");

        var validationResult = _validator.Validate(request);

        Assert.False(validationResult.IsValid);
    }
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void Should_Be_Invalid_When_Power_Is_Less_Than_One(int power)
    {
        var request = new CreatePredefinedProgramRequest(_name, _food, 10, power: power, _labelHeating, _instructions);

        var validationResult = _validator.Validate(request);

        Assert.False(validationResult.IsValid);
    }
    [Theory]
    [InlineData(11)]
    [InlineData(150)]
    [InlineData(int.MaxValue)]
    public void Should_Be_Invalid_When_Power_Is_Over_Ten(int power)
    {
        var request = new CreatePredefinedProgramRequest(_name, _food, 10, power: power, _labelHeating, _instructions);

        var validationResult = _validator.Validate(request);

        Assert.False(validationResult.IsValid);
    }
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void Should_Be_Invalid_When_Seconds_Is_Less_Than_One(int seconds)
    {
        var request = new CreatePredefinedProgramRequest(_name, _food, timeSeconds: seconds, 10, _labelHeating, _instructions);

        var validationResult = _validator.Validate(request);

        Assert.False(validationResult.IsValid);
    }
    [Theory]
    [InlineData(121)]
    [InlineData(150)]
    [InlineData(int.MaxValue)]
    public void Should_Be_Invalid_When_Seconds_Is_Over_120(int seconds)
    {
        var request = new CreatePredefinedProgramRequest(_name, _food, timeSeconds: seconds, 10, _labelHeating, _instructions);

        var validationResult = _validator.Validate(request);

        Assert.False(validationResult.IsValid);
    }
}
