using System.ComponentModel.DataAnnotations;
using DotNetBoilerplate.Application.Books.Update;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Books;

public class UpdateBookEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("{id:guid}", Handle)
            .RequireAuthorization()
            .WithSummary("Update a book");
    }

    private static async Task<Ok<Response>> Handle(
        [FromRoute] Guid id,
        [FromBody] Request request,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        var command = new UpdateBookCommand(
            id,
            request.Title,
            request.Writer,
            request.Genre,
            request.Year,
            request.Description
        );

        var result = await commandDispatcher.DispatchAsync<UpdateBookCommand, Guid>(command, ct);

        return TypedResults.Ok(new Response(result));
    }

    internal sealed record Response(
        Guid Id
    );

    internal sealed class Request
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Required] public string Title { get; init; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Required] public string Writer { get; init; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Required] public string Genre { get; init; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Required] public int Year { get; init; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Required] public string Description { get; init; }
    }
}