namespace Microwave.Core.Responses.PredefinedProgram;

public class PredefinedProgramResponse
{
    public PredefinedProgramResponse()
    {
    }

    public PredefinedProgramResponse(Models.PredefinedProgram predefinedProgram)
    {
        Id = predefinedProgram.Id;
        Name = predefinedProgram.Name;
        Food = predefinedProgram.Food;
        TimeSeconds = predefinedProgram.TimeSeconds;
        Power = predefinedProgram.Power;
        Instructions = predefinedProgram.Instructions;
        IsPredefined = predefinedProgram.IsPredefined;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Food { get; set; } = string.Empty;
    public int TimeSeconds { get; set; }
    public int Power { get; set; }
    public string? Instructions { get; set; }
    public bool IsPredefined { get; set; }
}
