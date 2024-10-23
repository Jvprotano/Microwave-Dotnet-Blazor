using Microwave.Core.Enums;
using Microwave.Core.Requests.Execution;
using Microwave.Core.Responses.Execution;

namespace Microwave.Core.Models;

public sealed class ExecutionControl
{
    private static ExecutionControl? _executionState = null;
    public static ExecutionControl GetInstance()
    {
        if (_executionState == null)
            _executionState = new ExecutionControl();

        return _executionState;
    }

    private ExecutionControl()
    {
        HeatingStatus = EExecutionStatus.STOPPED;
    }

    const int DEFAULT_ADD_TIME = 30;
    public EExecutionStatus HeatingStatus { get; private set; }
    public int Power { get; private set; }
    public int TimeRemaining { get; private set; }
    public int TotalTime { get; private set; }
    public char LabelHeating { get; private set; } = '.';
    private bool IsPredefinedProgram = false;

    public async Task Start(StartRequest request, char? labelHeating = null)
    {
        if (request.PredefinedProgramId.HasValue && HeatingStatus == EExecutionStatus.STOPPED)
        {
            IsPredefinedProgram = true;
            LabelHeating = labelHeating ?? '.';
        }
        else
            IsPredefinedProgram = false;

        if (HeatingStatus == EExecutionStatus.STOPPED)
        {
            TimeRemaining = request.Seconds;
            TotalTime = request.Seconds;
            Power = request.Power;

            HeatingStatus = EExecutionStatus.RUNNING;
            _ = StartHeating();
        }
        else if (HeatingStatus == EExecutionStatus.PAUSED)
        {
            HeatingStatus = EExecutionStatus.RUNNING;
            _ = StartHeating();
        }
        else if (HeatingStatus == EExecutionStatus.RUNNING && !IsPredefinedProgram)
        {
            TimeRemaining += DEFAULT_ADD_TIME;
            TotalTime += DEFAULT_ADD_TIME;
        }

        await Task.CompletedTask;
    }
    public async Task PauseOrStop()
    {
        if (!IsPredefinedProgram)
        {
            HeatingStatus = HeatingStatus switch
            {
                EExecutionStatus.RUNNING => EExecutionStatus.PAUSED,
                EExecutionStatus.PAUSED => Reset(),
                _ => EExecutionStatus.STOPPED
            };
        }

        await Task.CompletedTask;
    }
    private async Task StartHeating()
    {
        while (HeatingStatus == EExecutionStatus.RUNNING)
        {
            if (TimeRemaining > 0)
            {
                TimeRemaining--;
                await Task.Delay(1000);
            }
            else
                HeatingStatus = EExecutionStatus.STOPPED;
        }
    }
    private EExecutionStatus Reset()
    {
        TimeRemaining = 0;
        TotalTime = 0;
        Power = 0;
        LabelHeating = '.';

        return EExecutionStatus.STOPPED;
    }
    public ExecutionStatusResponse GetStatus()
    {
        var execStatus = new ExecutionStatusResponse(
                executionStatus: HeatingStatus,
                power: Power,
                totalTime: TotalTime,
                remainingTime: TimeRemaining,
                 labelHeatingChar: LabelHeating);

        execStatus.SetLabelHeating(GenerateLabelHeating());

        return execStatus;
    }

    private string GenerateLabelHeating()
    {
        List<string> labels = new();

        int elapsedTime = TotalTime - TimeRemaining;

        for (int i = 0; i < elapsedTime; i++)
        {
            labels.Add(new string(LabelHeating, Power));
        }

        var labelHeating = string.Join(" ", labels);

        return labelHeating;
    }
}
