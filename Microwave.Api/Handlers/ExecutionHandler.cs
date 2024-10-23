using Microwave.Api.Exceptions;
using Microwave.Api.Validators.Requests;
using Microwave.Core.Handlers;
using Microwave.Core.Models;
using Microwave.Core.Requests.Execution;
using Microwave.Core.Responses.Execution;

namespace Microwave.Api.Handlers;

public class ExecutionHandler(IPredefinedProgramHandler predefinedProgramHandler) : HandlerBase, IExecutionHandler
{
    private static ExecutionControl ExecutionControl
        => ExecutionControl.GetInstance();

    public ExecutionStatusResponse GetExecutionStatus()
    {
        ExecutionStatusResponse executionStatus = ExecutionControl.GetStatus();

        return executionStatus;
    }

    public async Task PauseOrCancelAsync()
    {
        await ExecutionControl.PauseOrStop();
    }

    public async Task StartAsync(StartRequest startRequest)
    {
        try
        {
            var validator = new StartRequestValidator();

            var validatorResult = await validator.ValidateAsync(startRequest);

            if (!validatorResult.IsValid)
                throw new MicrowaveValidationException(validatorResult.ToString());

            char? labelHeating = null;

            if (startRequest.PredefinedProgramId.HasValue)
            {
                var predefinedProgram = await predefinedProgramHandler.GetByIdAsync(startRequest.PredefinedProgramId.Value);

                startRequest = new StartRequest(
                    seconds: predefinedProgram.TimeSeconds,
                    power: predefinedProgram.Power,
                    predefinedProgramId: startRequest.PredefinedProgramId
                    );

                labelHeating = predefinedProgram.LabelHeating;
            }

            await ExecutionControl.Start(startRequest, labelHeating: labelHeating);
        }
        catch (MicrowaveValidationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            LogToFile(ex);
            throw;
        }
    }
}
