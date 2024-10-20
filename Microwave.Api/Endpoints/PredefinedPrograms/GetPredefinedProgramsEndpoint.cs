using Microwave.Api.Common.Api;
using Microwave.Core.Handlers;
using Microwave.Core.Responses.PredefinedProgram;

namespace Microwave.Api.Endpoints.PredefinedPrograms;

public class GetPredefinedProgramsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
                .WithName("Predefined Programs")
                .WithSummary("Retrieves all predefined programs (Default and custom)")
                .Produces<PredefinedProgramResponse>()
                .Produces(StatusCodes.Status404NotFound);

    public static async Task<IResult> HandleAsync(IPredefinedProgramHandler handler)
    {
        var programs = await handler.GetAll();

        if (programs.Any())
            return Results.Ok(programs);

        return Results.NotFound();
    }
}
