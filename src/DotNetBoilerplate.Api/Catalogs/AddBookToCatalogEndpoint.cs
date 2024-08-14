using DotNetBoilerplate.Application.Catalogs.AddBook;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Catalogs;

public class AddBookToCatalogEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("{catalogId:guid}/books", Handle)
            .RequireAuthorization()
            .WithSummary("Add book to catalog");
    }

    private static async Task<Ok<Response>> Handle(
        [FromRoute] Guid catalogId,
        [FromBody] Request request,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        var command = new AddBookToCatalogCommand(catalogId, request.BookId);
        var result = await commandDispatcher.DispatchAsync<AddBookToCatalogCommand, Guid>(command, ct);

        return TypedResults.Ok(new Response(result));
    }
    internal sealed record Response(
        Guid Id
    );

    private class Request
    {
        public Guid BookId { get; init; }
    }
}