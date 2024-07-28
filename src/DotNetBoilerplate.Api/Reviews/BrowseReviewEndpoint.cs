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
            .WithSummary("Browse all reviews");
    }

    private static async Task<Ok<IEnumerable<ReviewDto>>> Handle(
        [FromServices] IQueryDispatcher queryDispatcher,
        CancellationToken ct
    )
    {
        var query = new BrowseReviewsQuery();

        var result = await queryDispatcher.QueryAsync(query, ct);

        return TypedResults.Ok(result);
    }
}