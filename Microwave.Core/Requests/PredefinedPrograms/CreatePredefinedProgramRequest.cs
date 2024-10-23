using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Microwave.Core.Requests.PredefinedPrograms;

public class CreatePredefinedProgramRequest : BaseRequest
{
    public CreatePredefinedProgramRequest()
    {
    }

    public CreatePredefinedProgramRequest(string name, string food, int timeSeconds,
        int power, string labelHeating, string? instructions = null)
    {
        Name = name;
        Food = food;
        TimeSeconds = timeSeconds;
        Power = power;
        LabelHeating = labelHeating;
        Instructions = instructions;
    }

    public string Name { get; set; } = string.Empty;
    public string Food { get; set; } = string.Empty;
    [DefaultValue(30)]
    public int TimeSeconds { get; set; }
    [DefaultValue(10)]
    public int Power { get; set; }
    [DefaultValue(null)]
    public string? Instructions { get; set; }
    [DefaultValue('A')]
    [MaxLength(1, ErrorMessage = "Label Heating must be only one letter")]
    public string LabelHeating { get; set; } = string.Empty;
}
