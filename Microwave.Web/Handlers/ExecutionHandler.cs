using System.Net.Http.Json;

using Microwave.Core.Handlers;
using Microwave.Core.Requests.Execution;
using Microwave.Core.Responses;
using Microwave.Core.Responses.Execution;

namespace Microwave.Web.Handlers;

public class ExecutionHandler(IHttpClientFactory httpClientFactory) : IHttpExecutionHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpName);

    public async Task PauseOrCancelAsync()
    {
        await _client.PostAsync("/v1/pause-stop", null);
    }
    public async Task<ExecutionStatusResponse> GetExecutionStatus()
    {
        var response = await _client.GetFromJsonAsync<BaseResponse<ExecutionStatusResponse>>("/v1/get-execution-status");
        return response!.Data!;
    }

    public async Task<BaseResponse<string>> StartAsync(StartRequest startRequest)
    {
        var response = await _client.PostAsJsonAsync("/v1/start", startRequest);

        var content = await response.Content.ReadFromJsonAsync<BaseResponse<string>>();

        if (response.IsSuccessStatusCode)
            return content!;

        return new BaseResponse<string>(string.Empty, message: content?.Message);
    }
}
