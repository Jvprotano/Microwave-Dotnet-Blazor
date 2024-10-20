using System.Text.Json.Serialization;

using Microwave.Core.Enums;

namespace Microwave.Core.Responses.Execution;

public class ExecutionStatusResponse
{
    public ExecutionStatusResponse(EExecutionStatus executionStatus, int power, int totalTime, int remainingTime,
         char labelHeatingChar)
    {
        ExecutionStatus = executionStatus;
        Power = power;
        RemainingTime = remainingTime;
        LabelHarmChar = labelHeatingChar;
        TotalTime = totalTime;
    }
    public EExecutionStatus ExecutionStatus { get; }
    public int RemainingTime { get; }
    public int TotalTime { get; }
    public int Power { get; }

    [JsonIgnore]
    public char LabelHarmChar { get; }
    [JsonIgnore]
    private int ElapsedTime => TotalTime - RemainingTime;
    public string LabelHarm => GenerateLabelHarm();

    private string GenerateLabelHarm()
    {
        List<string> labels = new();

        for (int i = 0; i < ElapsedTime; i++)
        {
            labels.Add(new string(LabelHarmChar, Power));
        }

        var labelHarm = string.Join(" ", labels);
        return labelHarm;
    }
}
