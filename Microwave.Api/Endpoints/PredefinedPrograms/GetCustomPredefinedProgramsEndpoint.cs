using Microwave.Api.Common.Api;
using Microwave.Core.Handlers;
using Microwave.Core.Models;
using Microwave.Core.Responses;
using Microwave.Core.Responses.PredefinedProgram;

namespace Microwave.Api.Endpoints.PredefinedPrograms;

public class GetCustomPredefinedProgramsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("custom-programs", HandleAsync)
                .WithName("Custom Predefined Programs")
                .WithSummary("Retrieves all custom programs")
                .Produces<BaseResponse<PredefinedProgram>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);

    public static async Task<IResult> HandleAsync(IPredefinedProgramHandler handler)
    {
        var programs = await handler.GetCustomPrograms();

        if (programs.Any())
            return Results.Ok(new BaseResponse<IList<PredefinedProgram>>(programs));

        return Results.NotFound(new BaseResponse<string>(string.Empty, message: "Não existem programas personalizados cadastrados"));
    }
}
