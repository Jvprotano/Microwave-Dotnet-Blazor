using System.Net.Http.Json;

using Microwave.Core.Handlers;
using Microwave.Core.Models;
using Microwave.Core.Requests.Account;
using Microwave.Core.Responses;

namespace Microwave.Web.Handlers;

public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpName);
    public async Task<BaseResponse<string>> LoginAsync(LoginRequest request)
    {
        var response = await _client.PostAsJsonAsync("/v1/authentication/login", request);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<Token>();

            return new BaseResponse<string>(result?.AccessToken, "Login realizado com sucesso!");
        }

        return new BaseResponse<string>(string.Empty, "Dados inválidos");
    }

    public async Task<BaseResponse<string>> RegisterAsync(RegisterRequest request)
    {
        var result = await _client.PostAsJsonAsync("/v1/authentication/register", request);

        return result.IsSuccessStatusCode ?
            new BaseResponse<string>(result.Content.ToString(), "Registrado com sucesso!")
            : new BaseResponse<string>(null, "Erro ao registrar");
    }
}
