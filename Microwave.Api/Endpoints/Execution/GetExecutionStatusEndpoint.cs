using Microwave.Api.Common.Api;
using Microwave.Core.Handlers;
using Microwave.Core.Responses;
using Microwave.Core.Responses.Execution;

namespace Microwave.Api.Endpoints.Execution;

public class GetExecutionStatusEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("get-execution-status", Handle)
            .WithName("GetExecutionStatus")
            .WithSummary("Gets the current status of the execution (Heating)")
            .WithOrder(3)
            .Produces<BaseResponse<ExecutionStatusResponse>>()
            .Produces(StatusCodes.Status404NotFound);

    public static IResult Handle(IExecutionHandler handler)
    {
        var statusExecution = handler.GetExecutionStatus();

        if (statusExecution == null)
            return Results.NotFound();

        return Results.Ok(new BaseResponse<ExecutionStatusResponse>(statusExecution));
    }
}
