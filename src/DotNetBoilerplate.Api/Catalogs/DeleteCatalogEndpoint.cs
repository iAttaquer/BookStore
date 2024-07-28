using DotNetBoilerplate.Application.Catalogs.Delete;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Catalogs;

internal sealed class DeleteCatalogEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("{id:guid}", Handle)
            .RequireAuthorization()
            .WithSummary("Delete catalog by Id");
    }

    private static async Task<Ok<Response>> Handle(
        Guid id,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        var command = new DeleteCatalogCommand(id);

        await commandDispatcher.DispatchAsync<DeleteCatalogCommand>(command, ct);

        return TypedResults.Ok(new Response());
    }
    internal sealed record Response();
}