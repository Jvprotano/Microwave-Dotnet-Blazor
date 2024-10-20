using Microwave.Api.Common.Api;
using Microwave.Core.Handlers;
using Microwave.Core.Models;
using Microwave.Core.Responses;

namespace Microwave.Api.Endpoints.PredefinedPrograms;

public class DeleteCustomPredefinedProgramEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Delete Custom Predefined Program")
            .WithSummary("Removes a custom predefined program. It will not delete default programs")
            .Produces<BaseResponse<string>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

    public static async Task<IResult> HandleAsync(
        Guid id,
        IPredefinedProgramHandler handler)
    {
        try
        {
            await handler.DeleteCustomProgram(id);
            return Results.Ok(new BaseResponse<string>(string.Empty, "Deletado com sucesso"));
        }
        catch (Exception)
        {
            return Results.BadRequest(new BaseResponse<string>(string.Empty, "Erro ao deletar"));
        }
    }
}
