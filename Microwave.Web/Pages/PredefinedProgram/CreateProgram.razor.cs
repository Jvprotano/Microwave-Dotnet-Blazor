using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using Microwave.Core.Handlers;
using Microwave.Core.Requests.PredefinedPrograms;

using MudBlazor;

namespace Microwave.Web.Pages.PredefinedProgram;

public partial class CreateProgramPage() : ComponentBase
{
    #region Dependencies
    [Inject]
    public IHttpPredefinedProgramHandler Handler { get; set; } = null!;
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    #endregion
    public bool IsBusy { get; set; } = false;
    public CreatePredefinedProgramRequest InputModel { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity is null || !user.Identity.IsAuthenticated || string.IsNullOrEmpty(user.Identity.Name))
        {
            Snackbar.Add("You are not logged in.", Severity.Error);
            NavigationManager.NavigateTo("/login");
        }
    }
    public async Task OnValidSubmitAsync()
    {
        try
        {
            IsBusy = true;
            var result = await Handler.SaveCustomProgram(InputModel!);

            if (result.Data != null)
                Snackbar.Add(result.Message ?? "Programa criado com sucesso!", Severity.Success);
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
