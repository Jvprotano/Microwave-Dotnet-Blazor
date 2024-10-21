using Microsoft.AspNetCore.Components.WebAssembly.Http;

using Blazored.LocalStorage;

namespace Microwave.Web.Security;

public class CustomHttpHandler(ILocalStorageService localStorageService) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await localStorageService.GetItemAsync<string>(Configuration.DefaultTokenName);

        if (!string.IsNullOrEmpty(token))
            request.Headers.Add("Authorization", $"Bearer {token}");

        return await base.SendAsync(request, cancellationToken);
    }
}
