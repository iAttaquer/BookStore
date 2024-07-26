using System.ComponentModel.DataAnnotations;
using DotNetBoilerplate.Application.Catalogs.Create;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Catalogs;

internal sealed class CreateCatalogEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("", Handle)
            .RequireAuthorization()
            .WithSummary("Create catalog");
    }
    private static async Task<Ok<Response>> Handle(
        [FromBody] Request request,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        var command = new CreateCatalogCommand(
            request.Name,
            request.Genre,
            request.Description
        );

        var result = await commandDispatcher.DispatchAsync<CreateCatalogCommand, Guid>(command, ct);

        return TypedResults.Ok(new Response(result));
    }

    internal sealed record Response(
        Guid Id
    );

    private sealed class Request
    {
        [Required] public string Name { get; init; }

        [Required] public string Genre { get; init; }

        [Required] public string Description { get; init; }
    }
}
