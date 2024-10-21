using Microwave.Core.Requests.Execution;
using Microwave.Core.Responses;
using Microwave.Core.Responses.Execution;

namespace Microwave.Core.Handlers;

public interface IHttpExecutionHandler
{
    Task<BaseResponse<string>> StartAsync(StartRequest startRequest);
    Task PauseOrCancelAsync();
    Task<ExecutionStatusResponse> GetExecutionStatus();
}
