namespace Microwave.Core.Models;

public class PredefinedProgram : EntityBase
{
    public PredefinedProgram(string name, string food, int timeSeconds, int power, char labelHeating, string? instructions = null)
    {
        Name = name;
        Food = food;
        TimeSeconds = timeSeconds;
        Power = power;
        Instructions = instructions;
        LabelHeating = labelHeating;
        IsPredefined = false;
    }

    public string Name { get; private set; }
    public string Food { get; private set; }
    public int TimeSeconds { get; private set; }
    public int Power { get; private set; }
    public string? Instructions { get; private set; }
    public char LabelHeating { get; private set; }
    public bool IsPredefined { get; private set; }
}
