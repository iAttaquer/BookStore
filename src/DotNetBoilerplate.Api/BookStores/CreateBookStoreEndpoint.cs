using System.ComponentModel.DataAnnotations;
using DotNetBoilerplate.Application.BookStores.Create;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.BookStores;

public class CreateBookStoreEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("", Handle)
            .RequireAuthorization()
            .WithSummary("Create a new book store");
    }

    private static async Task<Ok<Response>> Handle(
        [FromBody] Request request,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        var command = new CreateBookStoreCommand(
            request.Name,
            request.Description
        );

        var result = await commandDispatcher.DispatchAsync<CreateBookStoreCommand, Guid>(command, ct);

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
        [Required] public string Description { get; init; }
    }
}