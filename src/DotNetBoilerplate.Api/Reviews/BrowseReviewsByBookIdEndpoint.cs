using DotNetBoilerplate.Application.Reviews.Browse;
using DotNetBoilerplate.Application.Reviews.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Reviews;

public class BrowseReviewByBookIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("book/{id:guid}", Handle)
            .WithSummary("Browse all reviews for the book");
    }

    private static async Task<Results<Ok<IEnumerable<ReviewDto>>, NotFound>> Handle(
        [FromRoute] Guid id,
        [FromServices] IQueryDispatcher queryDispatcher,
        CancellationToken ct
    )
    {
        var query = new BrowseReviewsByBookIdQuery(id);

        var result = await queryDispatcher.QueryAsync(query, ct);

        if (result is null) return TypedResults.NotFound();

        return TypedResults.Ok(result);
    }
}