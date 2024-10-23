using System.Net;
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
    public async Task<BaseResponse<PredefinedProgramResponse?>> SaveCustomProgram(CreatePredefinedProgramRequest request)
    {
        try
        {
            var result = await _client.PostAsJsonAsync("/v1/predefined-programs/create-predefined-program", request);
            BaseResponse<PredefinedProgramResponse?> response = null!;
            BaseResponse<string> responseError = new();

            if (result.IsSuccessStatusCode)
            {
                response = await result.Content.ReadFromJsonAsync<BaseResponse<PredefinedProgramResponse?>>()
                                ?? new BaseResponse<PredefinedProgramResponse?>(null, "Erro ao obter os dados.");
                return response;
            }
            else
                responseError = await result.Content.ReadFromJsonAsync<BaseResponse<string>>()
                                ?? new BaseResponse<string>(null, "Erro ao obter os dados.");

            return new BaseResponse<PredefinedProgramResponse?>(null, responseError.Message);
        }
        catch (Exception)
        {
            return new BaseResponse<PredefinedProgramResponse?>(null, "Erro ao realizar requisição");
        }
    }
}
