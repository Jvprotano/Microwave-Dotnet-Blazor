using Microsoft.AspNetCore.Components.Authorization;

namespace Microwave.Web.Security;

public interface IJwtAuthenticationStateProvider
{
    Task<bool> CheckAuthenticatedAsync();
    Task<AuthenticationState> GetAuthenticationStateAsync();
    void NotifyAuthenticationStateChanged();
}
