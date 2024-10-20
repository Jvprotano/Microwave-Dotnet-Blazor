using Microwave.Core.Enums;
using Microwave.Core.Models;
using Microwave.Core.Requests.Execution;

namespace Microwave.Tests.Microwave.Core.Tests.Models;

public class ExecutionControlTests
{
    private readonly ExecutionControl _executionControl = ExecutionControl.GetInstance();
    private readonly StartRequest _startRequest = new();

    private async Task StopExecution()
    {
        await _executionControl.PauseOrStop();
        await _executionControl.PauseOrStop();
    }
    [Fact]
    public async Task Should_Put_Running_Status()
    {
        await _executionControl.Start(_startRequest);
        var executionStatus = _executionControl.GetStatus();

        Assert.Equal(EExecutionStatus.RUNNING, executionStatus.status);
        await StopExecution();
    }
    [Fact]
    public async Task Should_Put_Pause_Status()
    {
        await _executionControl.Start(_startRequest);

        await _executionControl.PauseOrStop();

        var executionStatus = _executionControl.GetStatus();

        Assert.Equal(EExecutionStatus.PAUSED, executionStatus.status);
        await StopExecution();
    }
    [Fact]
    public async Task Should_Put_Stopped_Status()
    {
        await _executionControl.Start(_startRequest);

        await StopExecution();

        var executionStatus = _executionControl.GetStatus();

        Assert.Equal(EExecutionStatus.STOPPED, executionStatus.status);
    }
    [Fact]
    public async Task Should_Stop_When_Finish()
    {
        await _executionControl.Start(new StartRequest(seconds: 1));

        await Task.Delay(1500).WaitAsync(new CancellationToken());

        var executionStatus = _executionControl.GetStatus();

        Assert.Equal(EExecutionStatus.STOPPED, executionStatus.status);
    }
    [Fact]
    public async Task Should_Reset_Information_When_Stop()
    {
        await _executionControl.Start(_startRequest);

        var executionStatusBefore = _executionControl.GetStatus();

        await StopExecution();

        var executionStatusThan = _executionControl.GetStatus();

        Assert.NotEqual(executionStatusBefore.totalTime, executionStatusThan.totalTime);
        Assert.Equal(0, executionStatusThan.totalTime);
    }
}
