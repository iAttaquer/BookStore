using DotNetBoilerplate.Application.Books.Delete;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Books;

internal sealed class DeleteBookEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("{id:guid}", Handle)
            .RequireAuthorization()
            .WithSummary("Delete book by Id");
    }

    private static async Task<IResult> Handle(
        Guid id,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        var command = new DeleteBookCommand(id);

        await commandDispatcher.DispatchAsync<DeleteBookCommand>(command, ct);

        return Results.NoContent();
    }
}