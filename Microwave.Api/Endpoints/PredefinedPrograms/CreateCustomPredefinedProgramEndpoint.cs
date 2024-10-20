using Microsoft.AspNetCore.Http.HttpResults;

using Microwave.Api.Common.Api;
using Microwave.Api.Validators.Requests;
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
            .Produces<PredefinedProgram>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

    public static async Task<IResult> HandleAsync(
        CreatePredefinedProgramRequest request,
        IPredefinedProgramHandler handler)
    {
        var validator = new CreatePredefinedProgramValidator();

        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return Results.BadRequest(new BaseResponse<string>(string.Empty, message: validationResult.ToString()));

        var program = await handler.SaveCustomProgram(request);

        return Results.Ok(new BaseResponse<PredefinedProgram>(program));
    }
}
