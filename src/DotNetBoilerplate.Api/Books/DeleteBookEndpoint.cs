using DotNetBoilerplate.Application.Books.Delete;
using DotNetBoilerplate.Application.Books.DTO;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
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

    private static async Task<Ok<Response>> Handle(
        Guid id,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        var command = new DeleteBookCommand(id);

        await commandDispatcher.DispatchAsync<DeleteBookCommand>(command, ct);

        return TypedResults.Ok(new Response());
    }
    internal sealed record Response();
}