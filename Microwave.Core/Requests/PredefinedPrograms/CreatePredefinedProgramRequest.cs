namespace Microwave.Core.Requests.PredefinedPrograms;

public class CreatePredefinedProgramRequest : BaseRequest
{
    public string Name { get; private set; } = string.Empty;
    public string Food { get; private set; } = string.Empty;
    public int TimeSeconds { get; private set; }
    public int Power { get; private set; }
    public string? Instructions { get; private set; }
    public char LabelHarm { get; private set; }
}
