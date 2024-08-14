using DotNetBoilerplate.Application.Catalogs.Browse;
using DotNetBoilerplate.Application.Catalogs.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Catalogs;

public class BrowseCatalogsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("", Handle)
           .WithSummary("Browse catalogs with optional BookStoreId parameter");
    }

    private static async Task<Results<Ok<IEnumerable<CatalogDto>>, NotFound>> Handle(
        [FromQuery] Guid? bookStoreId,
        [FromServices] IQueryDispatcher queryDispatcher,
        CancellationToken ct
    )
    {
        var query = new BrowseCatalogsQuery(bookStoreId);
        var result = await queryDispatcher.QueryAsync(query, ct);

        return result is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(result);
    }
}