using Microwave.Api.Common.Api;
using Microwave.Api.Exceptions;
using Microwave.Core.Handlers;
using Microwave.Core.Models;
using Microwave.Core.Requests.PredefinedPrograms;
using Microwave.Core.Responses;

namespace Microwave.Api.Endpoints.PredefinedPrograms;

public class CreateCustomPredefinedProgramEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("create-predefined-program", HandleAsync)
            .WithName("Create Predefined Program")
            .WithSummary("Add a new predefined program to the microwave")
            .Produces<BaseResponse<PredefinedProgram>>(StatusCodes.Status200OK)
            .Produces<BaseResponse<string>>(StatusCodes.Status400BadRequest);

    public static async Task<IResult> HandleAsync(
        CreatePredefinedProgramRequest request,
        IPredefinedProgramHandler handler)
    {
        try
        {
            var program = await handler.SaveCustomProgram(request);

            return Results.Ok(new BaseResponse<PredefinedProgram>(program));
        }
        catch (MicrowaveValidationException ex)
        {
            return Results.BadRequest(new BaseResponse<string>(null, ex.Message));
        }
        catch (Exception)
        {
            return Results.BadRequest(new BaseResponse<string>(null, "Erro desconhecido"));
        }
    }
}
