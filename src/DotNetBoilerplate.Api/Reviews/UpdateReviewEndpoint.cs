using System.ComponentModel.DataAnnotations;
using DotNetBoilerplate.Application.Reviews.Update;
using DotNetBoilerplate.Core.Reviews;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Reviews;

public class UpdateReviewEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("{id:guid}", Handle)
            .RequireAuthorization()
            .WithSummary("Update a book (only for admin)");
    }

    private static async Task<Ok<Response>> Handle(
        [FromRoute] Guid id,
        [FromBody] Request request,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        var rating = new Rating(request.Rating);

        var command = new UpdateReviewCommand(
            id,
            request.Name,
            rating,
            request.Comment
        );

        var result = await commandDispatcher.DispatchAsync<UpdateReviewCommand, Guid>(command, ct);

        return TypedResults.Ok(new Response(result));
    }

    internal sealed record Response(
        Guid Id
    );

    private sealed class Request
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Required] public string Name { get; init; }
        [Required] public int Rating { get; init; }
        [Required] public string Comment { get; init; }
    }
}