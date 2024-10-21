using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using Blazored.LocalStorage;

using Microwave.Core.Handlers;
using Microwave.Core.Requests.Account;

using MudBlazor;

namespace Microwave.Web.Pages.Authentication;

public partial class LoginPage : ComponentBase
{
    #region Dependencies
    [Inject]
    public IAccountHandler Handler { get; set; } = null!;
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    [Inject]
    public ILocalStorageService ILocalStorageService { get; set; } = null!;
    #endregion
    public bool IsBusy { get; set; } = false;
    public LoginRequest InputModel { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity != null && user.Identity.IsAuthenticated && !string.IsNullOrEmpty(user.Identity.Name))
            NavigationManager.NavigateTo("/");
    }
    public async Task OnValidSubmitAsync()
    {
        try
        {
            IsBusy = true;
            var result = await Handler.LoginAsync(InputModel);

            if (!string.IsNullOrEmpty(result.Data))
            {
                await ILocalStorageService.SetItemAsync(Configuration.DefaultTokenName, result.Data);
                await AuthenticationStateProvider.GetAuthenticationStateAsync();
                NavigationManager.NavigateTo("/");
            }
            else
                Snackbar.Add(result.Message ?? "", Severity.Error);
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            IsBusy = false;
        }
    }
}
