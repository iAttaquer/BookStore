using DotNetBoilerplate.Application.Catalogs.Browse;
using DotNetBoilerplate.Application.Catalogs.DTO;
using DotNetBoilerplate.Application.Catalogs.Get;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Catalogs;

public class BrowseCatalogsByBookStoreIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("bookstore/{id:guid}", Handle)
            .WithSummary("Browse all Catalogs in Book Store");
    }

    private static async Task<Results<Ok<IEnumerable<CatalogDto>>, NotFound>> Handle(
        [FromRoute] Guid id,
        [FromServices] IQueryDispatcher queryDispatcher,
        CancellationToken ct
    )
    {
        var query = new BrowseCatalogsByBookStoreIdQuery(id);

        var result = await queryDispatcher.QueryAsync(query, ct);

        if (result is null) return TypedResults.NotFound();

        return TypedResults.Ok(result);
    }
}