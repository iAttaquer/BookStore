using System.ComponentModel.DataAnnotations;
using DotNetBoilerplate.Application.Catalogs.Update;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Catalogs;

public class UpdateCatalogEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("{id:guid}", Handle)
            .RequireAuthorization()
            .WithSummary("Update a catalog");
    }

    private static async Task<Ok<Response>> Handle(
        [FromRoute] Guid id,
        [FromBody] Request request,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        var command = new UpdateCatalogCommand(
            id,
            request.Name,
            request.Genre,
            request.Description
        );

        var result = await commandDispatcher.DispatchAsync<UpdateCatalogCommand, Guid>(command, ct);

        return TypedResults.Ok(new Response(result));
    }

    internal sealed record Response(
        Guid Id
    );

    private sealed class Request
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Required] public string Name { get; init; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Required] public string Genre { get; init; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Required] public string Description { get; init; }
    }
}