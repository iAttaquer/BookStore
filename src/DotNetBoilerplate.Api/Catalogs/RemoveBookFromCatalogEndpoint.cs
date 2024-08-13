using System.ComponentModel.DataAnnotations;
using DotNetBoilerplate.Application.Catalogs.Delete;
using DotNetBoilerplate.Application.Catalogs.RemoveBook;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Catalogs;

internal sealed class RemoveBookFromCatalogEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("{catalogId:guid}/books/{bookId:guid}", Handle)
            .RequireAuthorization()
            .WithSummary("Remove book from catalog by Id");
    }

    private static async Task<IResult> Handle(
        Guid catalogId, Guid bookId,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        var command = new RemoveBookFromCatalogCommand(catalogId, bookId);

        await commandDispatcher.DispatchAsync<RemoveBookFromCatalogCommand>(command, ct);

        return TypedResults.NoContent();
    }
    private sealed class Request
    {
        [Required] public Guid BookId { get; init; }
    }
}