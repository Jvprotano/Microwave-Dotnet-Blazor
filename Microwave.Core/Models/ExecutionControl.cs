using Microwave.Core.Enums;
using Microwave.Core.Requests.Execution;

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
        HarmStatus = EExecutionStatus.STOPPED;
    }

    const int DEFAULT_ADD_TIME = 30;
    public EExecutionStatus HarmStatus { get; private set; }
    public int Power { get; private set; }
    public int TimeRemaining { get; private set; }
    public int TotalTime { get; private set; }
    public char LabelHeating { get; private set; } = '.';


    public async Task Start(StartRequest request)
    {
        if (HarmStatus == EExecutionStatus.STOPPED)
        {
            TimeRemaining = request.Seconds;
            TotalTime = request.Seconds;
            Power = request.Power;
        }
        else if (HarmStatus == EExecutionStatus.RUNNING)
        {
            TimeRemaining += DEFAULT_ADD_TIME;
            TotalTime += DEFAULT_ADD_TIME;
        }

        HarmStatus = EExecutionStatus.RUNNING;
        _ = StartHeating();

        await Task.CompletedTask;
    }
    public void PauseOrStop()
    {
        HarmStatus = HarmStatus switch
        {
            EExecutionStatus.RUNNING => EExecutionStatus.PAUSED,
            EExecutionStatus.PAUSED => Reset(),
            _ => EExecutionStatus.STOPPED
        };
    }
    private async Task StartHeating()
    {
        while (HarmStatus == EExecutionStatus.RUNNING)
        {
            if (TimeRemaining > 0)
            {
                TimeRemaining--;
                await Task.Delay(1000);
            }
            else
                HarmStatus = EExecutionStatus.STOPPED;
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
    public (EExecutionStatus status,
            int power,
            int totalTime,
            int timeRemaining,
            char labelHeating) GetStatus()
            => (
                status: HarmStatus,
                power: Power,
                totalTime: TotalTime,
                timeRemaining: TimeRemaining,
                 labelHeating: LabelHeating);
}
