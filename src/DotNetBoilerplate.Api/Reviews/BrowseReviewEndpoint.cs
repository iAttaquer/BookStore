using DotNetBoilerplate.Application.Reviews.Browse;
using DotNetBoilerplate.Application.Reviews.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Reviews;

public class BrowseReviewEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("", Handle)
            .WithSummary("Browse all reviews or reviews for a specific book");
    }

    private static async Task<Results<Ok<IEnumerable<ReviewDto>>, NotFound>> Handle(
        [FromQuery] Guid? bookId,
        [FromServices] IQueryDispatcher queryDispatcher,
        CancellationToken ct
    )
    {
        var query = new BrowseReviewsQuery(bookId);

        var result = await queryDispatcher.QueryAsync(query, ct);

        return result is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(result);
    }
}
