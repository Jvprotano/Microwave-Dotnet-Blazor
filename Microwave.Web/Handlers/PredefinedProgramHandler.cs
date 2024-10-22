using System.Net.Http.Json;

using Microwave.Core.Handlers;
using Microwave.Core.Requests.PredefinedPrograms;
using Microwave.Core.Responses;
using Microwave.Core.Responses.PredefinedProgram;

namespace Microwave.Web.Handlers;

public class PredefinedProgramHandler(IHttpClientFactory httpClientFactory) : IHttpPredefinedProgramHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpName);

    public async Task<IList<PredefinedProgramResponse>> GetAll()
    {
        var result = await _client.GetAsync("/v1/predefined-programs");
        if (result.IsSuccessStatusCode)
        {
            var response = await result.Content.ReadFromJsonAsync<BaseResponse<IList<PredefinedProgramResponse>>>();
            return response!.Data!;
        }

        return [];
    }
    public Task<PredefinedProgramResponse> SaveCustomProgram(CreatePredefinedProgramRequest request)
    {
        throw new NotImplementedException();
    }
}
