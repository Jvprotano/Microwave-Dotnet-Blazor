using Microwave.Api.Common.Api;
using Microwave.Api.Exceptions;
using Microwave.Api.Validators.Requests;
using Microwave.Core.Handlers;
using Microwave.Core.Requests.Execution;
using Microwave.Core.Responses;

namespace Microwave.Api.Endpoints.Execution;

public class StartEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapPost("start", handler: HandleAsync)
            .WithName("Start")
            .WithSummary("Start heating using the default of 30 seconds and power 10 if not specified")
            .WithOrder(1)
            .Produces(StatusCodes.Status200OK)
            .Produces<BaseResponse<string>>(StatusCodes.Status400BadRequest);

    private static async Task<IResult> HandleAsync(
        StartRequest request,
        IExecutionHandler handler
    )
    {
        try
        {
            await handler.StartAsync(request);

            return Results.Ok(new BaseResponse<string>(data: "Operação realizada", message: "Iniciado com sucesso!"));
        }
        catch (MicrowaveValidationException ex)
        {
            return Results.BadRequest(new BaseResponse<string>(string.Empty, ex.Message));
        }
        catch (Exception)
        {
            return Results.BadRequest(new BaseResponse<string>(string.Empty, "Erro desconhecido"));
        }

    }
}
