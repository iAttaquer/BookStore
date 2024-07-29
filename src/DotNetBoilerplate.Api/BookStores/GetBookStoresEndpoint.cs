using DotNetBoilerplate.Application.BookStores.Get;
using DotNetBoilerplate.Application.BookStores.Browse;
using DotNetBoilerplate.Application.BookStores.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.BookStores;

public class GetBookStoresEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("", Handle)
            .WithSummary("Browse book store with optional Id parameter");
    }

    private static async Task<Results<Ok<IEnumerable<BookStoreDto>>, Ok<BookStoreDto>, NotFound>> Handle(
        [FromQuery] Guid? id,
        [FromServices] IQueryDispatcher queryDispatcher,
        CancellationToken ct
    )
    {
        if (id.HasValue)
        {
            var query = new GetBookStoreByIdQuery(id.Value);

            var result = await queryDispatcher.QueryAsync(query, ct);

            if (result is null) return TypedResults.NotFound();

            return TypedResults.Ok(result);
        }
        else
        {
            var query = new BrowseBookStoresQuery();

            var result = await queryDispatcher.QueryAsync(query, ct);

            return TypedResults.Ok(result);
        }
    }
}