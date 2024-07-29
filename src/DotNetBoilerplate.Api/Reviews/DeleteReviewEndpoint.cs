using DotNetBoilerplate.Application.Reviews.Delete;
using DotNetBoilerplate.Application.Reviews.DTO;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Reviews;

internal sealed class DeleteReviewEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("{id:guid}", Handle)
            .RequireAuthorization()
            .WithSummary("Delete review by Id");
    }

    private static async Task<IResult> Handle(
        Guid id,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        var command = new DeleteReviewCommand(id);

        await commandDispatcher.DispatchAsync<DeleteReviewCommand>(command, ct);

        return Results.NoContent();
    }
}