using DotNetBoilerplate.Application.Carts.Delete;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Carts;

internal sealed class DeleteCartEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("{cartId:guid}", Handle)
            .RequireAuthorization()
            .WithSummary("Delete cart by Id or remove item from it");
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid cartId,
        [FromQuery] Guid? bookId,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        var command = new DeleteCommand(cartId, bookId);

        await commandDispatcher.DispatchAsync<DeleteCommand>(command, ct);

        return Results.NoContent();
    }
}