using System.Timers;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using Microwave.Core.Enums;
using Microwave.Core.Handlers;
using Microwave.Core.Requests.Account;

using MudBlazor;

namespace Microwave.Web.Pages;

public partial class MicrowavePage : ComponentBase
{
    [Inject]
    public IHttpExecutionHandler Handler { get; set; } = null!;
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    public RegisterRequest InputModel { get; set; } = new();

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
        // IniciarTimer();
    }
    public string LabelHeating = "Parado"; // Status atual do micro-ondas
    public int TimeRemaining; // Tempo restante durante o aquecimento
    public int TimeInput = 30; // Tempo de aquecimento definido pelo usuário
    public int PowerLevel = 10; // Potência atual do micro-ondas
    public string SelectedProgram = string.Empty; // Programa pré-definido selecionado
    public List<Programa> Programas = new(); // Lista de programas retornados pela API
    private System.Timers.Timer statusTimer = new();
    public EExecutionStatus ExecutionStatus { get; set; }


    // Função para iniciar o micro-ondas
    public async Task StartMicrowave()
    {
        if (TimeInput <= 0)
            return;

        await Handler.StartAsync(new(seconds: TimeInput, power: PowerLevel));
        IniciarTimer();
    }

    public async Task PauseMicrowave()
    {
        await Handler.PauseOrCancelAsync();
    }
    private void IniciarTimer()
    {
        statusTimer = new System.Timers.Timer(1000); // Verifica o status a cada 1 segundo
        statusTimer.Elapsed += VerificarStatus!;
        statusTimer.AutoReset = true;
        statusTimer.Start();
    }
    public void ProgramSelected(string program)
    {
        // var selectedProgram = Programas.FirstOrDefault(p => p.Nome == program);
        // if (selectedProgram != null)
        // {
        //     TimeInput = selectedProgram.TempoSegundos;
        //     PowerLevel = selectedProgram.Potencia;
        // }
    }
    // Função para verificar o status do micro-ondas
    private async void VerificarStatus(object sender, ElapsedEventArgs e)
    {
        var response = await Handler.GetExecutionStatus();
        if (response != null)
        {
            Console.WriteLine(response.ExecutionStatus);
            TimeRemaining = response.RemainingTime;
            LabelHeating = response.LabelHeating;
            ExecutionStatus = response.ExecutionStatus;

            if (response.ExecutionStatus == EExecutionStatus.STOPPED)
                statusTimer.Stop();
        }


    }
    private async Task CarregarProgramasAsync()
    {
        await Task.CompletedTask;
        // Programas = await HttpClient.GetFromJsonAsync<List<Programa>>("/api/microwave/programas");
    }

    // public void Dispose()
    // {
    //     statusTimer?.Dispose();
    // }

    // Classe representando um programa de micro-ondas
    public class Programa
    {
        public string Nome { get; set; } = string.Empty;
        public int TempoSegundos { get; set; }
        public string Potencia { get; set; } = string.Empty;
    }

    // Classe representando o status do micro-ondas
    public class MicrowaveStatus
    {
        public string Status { get; set; } = string.Empty;
        public int TimeRemaining { get; set; }
    }
}