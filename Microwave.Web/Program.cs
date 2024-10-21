using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Blazored.LocalStorage;

using Microwave.Core.Handlers;
using Microwave.Web;
using Microwave.Web.Handlers;
using Microwave.Web.Security;

using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
builder.Services.AddScoped(c => (IJwtAuthenticationStateProvider)c.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddTransient<IAccountHandler, AccountHandler>();
builder.Services.AddTransient<IHttpExecutionHandler, ExecutionHandler>();

builder.Services.AddMudServices();

builder.Services.AddScoped<CustomHttpHandler>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddHttpClient(name: Configuration.HttpName, opt =>
{
    opt.BaseAddress = new Uri(Configuration.BackendUrl);
}).AddHttpMessageHandler<CustomHttpHandler>();

await builder.Build().RunAsync();
