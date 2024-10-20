using Microsoft.EntityFrameworkCore;

using Microwave.Api.Data;
using Microwave.Api.Endpoints;
using Microwave.Api.Handlers;
using Microwave.Core.Handlers;

var builder = WebApplication.CreateBuilder(args);

string connString = builder.Configuration.GetConnectionString("Default") ?? string.Empty;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(connString);
});

builder.Services.AddTransient<IExecutionHandler, ExecutionHandler>();
builder.Services.AddTransient<IPredefinedProgramHandler, PredefinedProgramHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapEndpoints();

app.MapGet("/", () => new { message = "OK" })
    .WithOrder(999);

app.Run();
