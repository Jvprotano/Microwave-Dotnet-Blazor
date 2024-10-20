using Microwave.Api.Common.Api;
using Microwave.Core.Handlers;
using Microwave.Core.Requests.Execution;

namespace Microwave.Api.Common.Endpoints.Execution;

public class StartEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapPost("start", handler: HandleAsync)
            .WithName("Start")
            .WithSummary("Start heating using the default of 30 seconds and power 10 if not specified")
            .WithOrder(1)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

    private static async Task<IResult> HandleAsync(
        StartRequest request,
        IExecutionHandler handler
    )
    {
        await handler.StartAsync(request);
        return Results.NoContent();
    }
}
