using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Microwave.Core.Requests.Execution;

public class StartRequest : BaseRequest
{
    public StartRequest(int seconds = 30, int power = 10, Guid? predefinedProgramId = null)
    {
        Seconds = seconds;
        Power = power;
        PredefinedProgramId = predefinedProgramId;
    }

    [DefaultValue(30)]
    public int Seconds { get; set; } = 30;
    [Range(1, 10, ErrorMessage = "Potência deve estar entre 1 e 10")]
    public int Power { get; set; } = 10;
    [DefaultValue(null)]
    public Guid? PredefinedProgramId { get; set; }
}
