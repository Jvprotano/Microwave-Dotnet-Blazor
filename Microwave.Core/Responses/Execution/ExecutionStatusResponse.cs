using System.Text.Json.Serialization;

using Microwave.Core.Enums;

namespace Microwave.Core.Responses.Execution;

public class ExecutionStatusResponse
{
    public ExecutionStatusResponse(EExecutionStatus executionStatus, int power, int totalTime, int remainingTime,
         char labelHeatingChar, string labelHeating = ".")
    {
        ExecutionStatus = executionStatus;
        Power = power;
        RemainingTime = remainingTime;
        LabelHeatingChar = labelHeatingChar;
        TotalTime = totalTime;
        LabelHeating = labelHeating;
    }
    public EExecutionStatus ExecutionStatus { get; }
    public int RemainingTime { get; }
    public int TotalTime { get; }
    public int Power { get; }

    [JsonIgnore]
    public char LabelHeatingChar { get; }
    public string LabelHeating { get; set; }

    public void SetLabelHeating(string labelHeating)
    {
        LabelHeating = labelHeating;
    }
}
