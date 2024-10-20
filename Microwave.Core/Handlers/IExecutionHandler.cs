using Microwave.Core.Requests.Execution;
using Microwave.Core.Responses.Execution;

namespace Microwave.Core.Handlers;

public interface IExecutionHandler
{
    Task StartAsync(StartRequest startRequest);
    Task PauseOrCancelAsync();
    ExecutionStatusResponse GetExecutionStatus();
}
