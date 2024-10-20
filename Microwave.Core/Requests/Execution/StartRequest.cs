using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Microwave.Core.Requests.Execution;

public class StartRequest : BaseRequest
{
    [DefaultValue(30)]
    public int Seconds { get; set; } = 30;
    [Range(1, 10, ErrorMessage = "Potência deve estar entre 1 e 10")]
    public int Power { get; set; } = 10;
}
