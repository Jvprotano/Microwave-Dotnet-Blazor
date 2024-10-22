using System.Timers;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using Microwave.Core.Enums;
using Microwave.Core.Handlers;
using Microwave.Core.Models;
using Microwave.Core.Requests.Execution;
using Microwave.Core.Responses.PredefinedProgram;

using MudBlazor;

namespace Microwave.Web.Pages;

public partial class MicrowavePage : ComponentBase
{
    [Inject]
    public IHttpExecutionHandler Handler { get; set; } = null!;
    [Inject]
    public IHttpPredefinedProgramHandler PredefinedProgramHandler { get; set; } = null!;
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    public StartRequest InputModel { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity is null || !user.Identity.IsAuthenticated || string.IsNullOrEmpty(user.Identity.Name))
        {
            Snackbar.Add("You are not logged in.", Severity.Error);
            NavigationManager.NavigateTo("/login");
        }

        await CarregarProgramasAsync();
    }
    public string LabelHeating = ".";
    public int TimeRemaining;
    // public int TimeInput = 30;
    // public int PowerLevel = 10;
    public Guid? SelectedProgramId;
    public List<PredefinedProgramResponse> Programas = new();
    private System.Timers.Timer statusTimer = new();
    public EExecutionStatus ExecutionStatus { get; set; }

    public async Task StartMicrowave()
    {
        if (InputModel.Seconds <= 0)
            return;

        var response = await Handler.StartAsync(new(seconds: InputModel.Seconds, power: InputModel.Power, predefinedProgramId: InputModel.PredefinedProgramId));

        if (string.IsNullOrEmpty(response.Data))
            Snackbar.Add(response.Message ?? "Erro ao iniciar", Severity.Warning);

        if (!statusTimer.Enabled)
            IniciarTimer();
    }
    private void IniciarTimer()
    {
        statusTimer = new System.Timers.Timer(1000);
        statusTimer.Elapsed += VerificarStatus!;
        statusTimer.AutoReset = true;

        statusTimer.Start();
    }
    private async void VerificarStatus(object sender, ElapsedEventArgs e)
    {
        var response = await Handler.GetExecutionStatus();
        if (response != null)
        {
            TimeRemaining = response.RemainingTime;
            LabelHeating = response.LabelHeating;
            ExecutionStatus = response.ExecutionStatus;

            await InvokeAsync(StateHasChanged);

            if (response.ExecutionStatus == EExecutionStatus.STOPPED
                || response.ExecutionStatus == EExecutionStatus.PAUSED)
                Dispose();
        }
    }

    public async Task PauseMicrowave()
    {
        await Handler.PauseOrCancelAsync();
    }
    private async Task CarregarProgramasAsync()
    {
        var programs = await PredefinedProgramHandler.GetAll();

        if (programs.Any())
        {
            Programas = programs.ToList();
            await InvokeAsync(StateHasChanged);
        }
    }

    public void Dispose()
    {
        statusTimer.Stop();
        statusTimer?.Dispose();
    }
}