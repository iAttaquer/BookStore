
using System.ComponentModel.DataAnnotations;
using DotNetBoilerplate.Application.Reviews.Create;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Application.Reviews.Create;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using DotNetBoilerplate.Core.Reviews;

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
        [FromQuery] Guid? BookId,
        [FromBody] Request request,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        var rating = new Rating(request.Rating);

        var command = new CreateReviewCommand(
            rating,
            request.Comment,
            BookId
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
    }
}