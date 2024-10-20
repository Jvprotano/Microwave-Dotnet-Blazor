using Microwave.Api.Common.Endpoints;
using Microwave.Api.Handlers;
using Microwave.Core.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.CustomSchemaIds(type => type.FullName));

builder.Services.AddTransient<IExecutionHandler, ExecutionHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapEndpoints();

app.MapGet("/", () => new { message = "OK" }).WithOrder(999);

// app.MapGet("/v1/", () => "Hello World!");

// Get -> PredefinedProgram -> Pego os padrões e os salvos => IList<PredefinedProgram>
// Post -> PredefinedProgram -> Request => PredefinedProgram

app.Run();
