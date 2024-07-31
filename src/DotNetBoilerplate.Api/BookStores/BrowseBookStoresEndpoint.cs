using DotNetBoilerplate.Application.BookStores.Browse;
using DotNetBoilerplate.Application.BookStores.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.BookStores;

public class BrowseBookStoresEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("", Handle)
            .WithSummary("Browse all book stores");
    }

    private static async Task<Results<Ok<IEnumerable<BookStoreDto>>, NotFound>> Handle(
        [FromServices] IQueryDispatcher queryDispatcher,
        CancellationToken ct
    )
    {
        var query = new BrowseBookStoresQuery();

        var result = await queryDispatcher.QueryAsync(query, ct);

        return TypedResults.Ok(result);
    }
}
