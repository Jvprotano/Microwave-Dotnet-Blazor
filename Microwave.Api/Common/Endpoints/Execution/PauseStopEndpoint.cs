using Microwave.Api.Common.Api;
using Microwave.Core.Handlers;
using Microwave.Core.Responses;

namespace Microwave.Api.Common.Endpoints.Execution;

public class PauseStopEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapPost("pause-stop", handler: HandleAsync)
        .WithName("Pause or Cancel")
        .WithSummary("Pause if running and stop if paused the execution")
        .WithOrder(2)
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);


    public static async Task<IResult> HandleAsync(IExecutionHandler handler)
    {
        await handler.PauseOrCancelAsync();

        var statusExecution = handler.GetExecutionStatus();

        if (statusExecution == null)
            return Results.NotFound();

        return Results.NoContent();
    }
}
