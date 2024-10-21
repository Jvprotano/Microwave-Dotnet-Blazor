using Microwave.Api.Common.Api;
using Microwave.Api.Endpoints.Execution;
using Microwave.Api.Endpoints.PredefinedPrograms;

namespace Microwave.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("v1/")
        .RequireAuthorization()
        ;

        endpoints.MapGroup("predefined-programs")
            .WithTags("PredefinedPrograms")
            .MapEndpoint<CreateCustomPredefinedProgramEndpoint>()
            .MapEndpoint<GetCustomPredefinedProgramsEndpoint>()
            .MapEndpoint<GetPredefinedProgramsEndpoint>()
            .MapEndpoint<DeleteCustomPredefinedProgramEndpoint>()
        ;

        endpoints.MapGroup("/")
            .WithTags("Execution")
            .MapEndpoint<StartEndpoint>()
            .MapEndpoint<PauseStopEndpoint>()
            .MapEndpoint<GetExecutionStatusEndpoint>()
            ;
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}
