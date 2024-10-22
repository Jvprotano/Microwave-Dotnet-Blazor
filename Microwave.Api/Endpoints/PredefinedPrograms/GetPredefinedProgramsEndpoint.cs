using System.Text.Json;

using Microwave.Api.Common.Api;
using Microwave.Core.Handlers;
using Microwave.Core.Responses;
using Microwave.Core.Responses.PredefinedProgram;

namespace Microwave.Api.Endpoints.PredefinedPrograms;

public class GetPredefinedProgramsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
                .WithName("Predefined Programs")
                .WithSummary("Retrieves all predefined programs (Default and custom)")
                .Produces<BaseResponse<PredefinedProgramResponse>>(StatusCodes.Status200OK)
                .Produces<BaseResponse<string>>(StatusCodes.Status404NotFound);

    public static async Task<IResult> HandleAsync(IPredefinedProgramHandler handler)
    {
        var programs = await handler.GetAll();

        if (programs.Any())
        {
            IList<PredefinedProgramResponse> responseDto = [];
            
            programs.ToList().ForEach(program =>
            {
                responseDto.Add(new PredefinedProgramResponse(program));
            });

            return Results.Ok(new BaseResponse<IList<PredefinedProgramResponse>>(responseDto));
        }

        return Results.NotFound(new BaseResponse<string>(null, "Nenhum programa encontrado"));
    }
}
