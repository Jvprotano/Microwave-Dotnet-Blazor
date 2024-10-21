using Microwave.Api.Exceptions;
using Microwave.Api.Validators.Requests;
using Microwave.Core.Enums;
using Microwave.Core.Handlers;
using Microwave.Core.Models;
using Microwave.Core.Requests.Execution;
using Microwave.Core.Responses.Execution;

namespace Microwave.Api.Handlers;

public class ExecutionHandler(IPredefinedProgramHandler predefinedProgramHandler) : IExecutionHandler
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
    {
        var validator = new StartRequestValidator();

        var validatorResult = await validator.ValidateAsync(startRequest);

        if (!validatorResult.IsValid)
            throw new MicrowaveValidationException(validatorResult.ToString());

        if (startRequest.PredefinedProgramId.HasValue)
        {
            var predefinedProgram = await predefinedProgramHandler.GetByIdAsync(startRequest.PredefinedProgramId.Value);

            startRequest = new StartRequest(
                seconds: predefinedProgram.TimeSeconds,
                power: predefinedProgram.Power,
                predefinedProgramId: startRequest.PredefinedProgramId,
                predefinedProgram: predefinedProgram);
        }

        await ExecutionControl.Start(startRequest);
    }
}
