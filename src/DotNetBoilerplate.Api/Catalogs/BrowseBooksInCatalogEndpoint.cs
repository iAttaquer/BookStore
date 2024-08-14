using DotNetBoilerplate.Application.Catalogs.BrowseBooks;
using DotNetBoilerplate.Application.Books.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Catalogs;

public class BrowseBooksInCatalogEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("{catalogId:guid}/books", Handle)
           .WithSummary("Browse books with CatalogId parameter");
    }

    private static async Task<Results<Ok<IEnumerable<BookDto>>, NotFound>> Handle(
        [FromRoute] Guid catalogId,
        [FromServices] IQueryDispatcher queryDispatcher,
        CancellationToken ct
    )
    {
        var query = new BrowseBooksInCatalogQuery(catalogId);
        var result = await queryDispatcher.QueryAsync(query, ct);

        return result is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(result);
    }
}