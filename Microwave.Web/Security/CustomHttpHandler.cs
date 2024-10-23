using System.Net;
using System.Net.Http.Headers;

using Microsoft.AspNetCore.Components;

using Blazored.LocalStorage;

namespace Microwave.Web.Security;

public class CustomHttpHandler(ILocalStorageService localStorageService, NavigationManager navigationManager) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            var token = await localStorageService.GetItemAsync<string>(Configuration.DefaultTokenName);

            if (!string.IsNullOrEmpty(token))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                await RemoveAuthAndRedirect(navigationManager);

            return response;
        }
        catch (HttpRequestException)
        {
            await RemoveAuthAndRedirect(navigationManager);

            return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
            {
                ReasonPhrase = "Erro ao tentar se comunicar com o servidor."
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
    private async Task RemoveAuthAndRedirect(NavigationManager navigationManager)
    {
        await localStorageService.RemoveItemAsync(Configuration.DefaultTokenName);
        navigationManager.NavigateTo("/login");
    }
}
