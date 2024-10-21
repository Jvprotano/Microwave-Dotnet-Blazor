using Microwave.Core.Enums;
using Microwave.Core.Handlers;
using Microwave.Core.Models;
using Microwave.Core.Requests.Execution;
using Microwave.Core.Responses.Execution;

namespace Microwave.Api.Handlers;

public class ExecutionHandler : IExecutionHandler
{
    private static ExecutionControl ExecutionControl
        => ExecutionControl.GetInstance();

    public ExecutionStatusResponse GetExecutionStatus()
    {
        (EExecutionStatus status,
            int power,
            int totalTime,
            int remainingTime,
            char labelHeating) = ExecutionControl.GetStatus();

        ExecutionStatusResponse executionStatus = new(executionStatus: status, power: power, totalTime: totalTime, remainingTime: remainingTime,
            labelHeatingChar: labelHeating);

        return executionStatus;
    }

    public async Task PauseOrCancelAsync()
    {
        await ExecutionControl.PauseOrStop();
    }

    public async Task StartAsync(StartRequest startRequest)
        => await ExecutionControl.Start(startRequest);
}
