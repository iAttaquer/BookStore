
using System.ComponentModel.DataAnnotations;
using DotNetBoilerplate.Application.Reviews.Create;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Application.Reviews.Create;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Reviews;

public class CreateReviewEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("", Handle)
            .RequireAuthorization()
            .WithSummary("Create a new review");
    }

    private static async Task<Ok<Response>> Handle(
        [FromBody] Request request,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        var command = new CreateReviewCommand(
            request.Rating,
            request.Comment,
            request.BookId
        );

        var result = await commandDispatcher.DispatchAsync<CreateReviewCommand, Guid>(command, ct);

        return TypedResults.Ok(new Response(result));
    }

    internal sealed record Response(
        Guid Id
    );

    private sealed class Request
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Required] public int Rating { get; init; }
        [Required] public string Comment { get; init; }
        [Required] public Guid BookId  { get; init; }
    }
}