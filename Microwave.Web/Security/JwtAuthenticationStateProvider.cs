using System.Security.Claims;

using Microsoft.AspNetCore.Components.Authorization;

using Blazored.LocalStorage;

namespace Microwave.Web.Security;

public class JwtAuthenticationStateProvider(ILocalStorageService _localStorageService) : AuthenticationStateProvider, IJwtAuthenticationStateProvider
{
    private bool _isAuthenticated = false;
    public async Task<bool> CheckAuthenticatedAsync()
    {
        await GetAuthenticationStateAsync();
        return _isAuthenticated;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var jwt = await _localStorageService.GetItemAsync<string>(Configuration.DefaultTokenName);

        if (string.IsNullOrEmpty(jwt))
        {
            _isAuthenticated = false;
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var claim = new List<Claim>(){
            new Claim(ClaimTypes.Name, jwt)
        };

        return new AuthenticationState(new ClaimsPrincipal(
            new ClaimsIdentity(claim, "Bearer")));
    }

    public void NotifyAuthenticationStateChanged()
        => base.NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
}
