using Microwave.Api.Common.Api;
using Microwave.Api.Common.Endpoints.Execution;

namespace Microwave.Api.Common.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        // Tudo que faço aqui aplico para todas as rotas
        var endpoints = app.MapGroup("v1/")
            //.RequireAuthorization()
        ;

        endpoints.MapGroup("v1/predefined-programs")
        .WithTags("PredefinedPrograms");

        endpoints.MapGroup("/")
            .WithTags("Execution")
            .MapEndpoint<StartEndpoint>()
            .MapEndpoint<PauseStopEndpoint>()
            .MapEndpoint<GetExecutionStatusEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}
