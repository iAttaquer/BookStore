using System.ComponentModel.DataAnnotations;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Application.Carts.Create;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Carts;

public class CreateCartEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("", Handle)
            .RequireAuthorization()
            .WithSummary("Create the cart");
    }

    private static async Task<Ok<Response>> Handle(
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        var command = new CreateCartCommand();
        var result = await commandDispatcher.DispatchAsync<CreateCartCommand, Guid>(command, ct);
        return TypedResults.Ok(new Response(result));
    }

    internal sealed record Response(
        Guid Id
    );
}