using DotNetBoilerplate.Application.Reviews.Get;
using DotNetBoilerplate.Application.Reviews.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Reviews;

public class GetReviewByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("{id:guid}", Handle)
            .WithSummary("Get review by Id");
    }

    private static async Task<Results<Ok<ReviewDto>, NotFound>> Handle(
        [FromRoute] Guid id,
        [FromServices] IQueryDispatcher queryDispatcher,
        CancellationToken ct
    )
    {
        var query = new GetReviewByIdQuery(id);

        var result = await queryDispatcher.QueryAsync(query, ct);

        if (result is null) return TypedResults.NotFound();

        return TypedResults.Ok(result);
    }
}