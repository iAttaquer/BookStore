using System.ComponentModel.DataAnnotations;
using DotNetBoilerplate.Application.Carts.Update;
using DotNetBoilerplate.Core.Carts;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Carts;

public class UpdateCartEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("{bookId:guid}", Handle)
            .RequireAuthorization()
            .WithSummary("Update a cart - add an item");
    }

    private static async Task<Ok<Response>> Handle(
        [FromRoute] Guid bookId,
        [FromBody] Request request,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct 
    )
    {
        var quantity = new Quantity(request.Quantity);

        var command = new UpdateCartCommand(
            bookId,
            quantity
        );

        var result = await commandDispatcher.DispatchAsync<UpdateCartCommand, Guid>(command, ct);

        return TypedResults.Ok(new Response(result));
    }

    internal sealed record Response(
        Guid Id
    );

    internal sealed class Request
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Required] public int Quantity { get; init; }
    }
}