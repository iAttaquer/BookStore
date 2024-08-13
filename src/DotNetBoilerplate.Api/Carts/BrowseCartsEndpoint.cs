using DotNetBoilerplate.Application.Carts.Browse;
using DotNetBoilerplate.Application.Carts.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Carts;

public class BrowseCartsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/all", Handle)
            .RequireAuthorization()
            .WithSummary("Browse all carts");
    }

    private static async Task<Ok<IEnumerable<CartDto>>> Handle(
        [FromServices] IQueryDispatcher queryDispatcher,
        CancellationToken ct
    )
    {
        var query = new BrowseCartsQuery();

        var result = await queryDispatcher.QueryAsync(query, ct);

        return TypedResults.Ok(result);
    }
}