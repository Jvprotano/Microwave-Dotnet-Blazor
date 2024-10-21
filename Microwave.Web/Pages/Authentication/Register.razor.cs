using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using Microwave.Core.Handlers;
using Microwave.Core.Requests.Account;

using MudBlazor;

namespace Microwave.Web.Pages.Authentication;

public partial class RegisterPage() : ComponentBase
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
    #endregion
    public bool IsBusy { get; set; } = false;
    public RegisterRequest InputModel { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity is { IsAuthenticated: true })
            NavigationManager.NavigateTo("/");
    }
    public async Task OnValidSubmitAsync()
    {
        try
        {
            IsBusy = true;
            var result = await Handler.RegisterAsync(InputModel);

            if (!string.IsNullOrEmpty(result.Data))
            {
                Snackbar.Add(result.Message ?? "Registrado com sucesso!", Severity.Success);
                NavigationManager.NavigateTo("/login");

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
