using System.ComponentModel.DataAnnotations;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Application.Books.Create;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Books;

public class CreateBookEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("", Handle)
            .RequireAuthorization()
            .WithSummary("Create a new book");
    }

    private static async Task<Ok<Response>> Handle(
        [FromBody] Request request,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        var command = new CreateBookCommand(
            request.Title,
            request.Writer,
            request.Genre,
            request.Year,
            request.Description
        );

        var result = await commandDispatcher.DispatchAsync<CreateBookCommand, Guid>(command, ct);

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