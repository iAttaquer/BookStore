using DotNetBoilerplate.Application.Books.Browse;
using DotNetBoilerplate.Application.Books.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Books;

public class BrowseBooksEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("", Handle)
            .WithSummary("Browse books with optional BookStoreId parameter");
    }

    private static async Task<Results<Ok<IEnumerable<BookDto>>, Ok<BookDto>, NotFound>> Handle(
        [FromQuery] Guid? bookStoreId,
        [FromServices] IQueryDispatcher queryDispatcher,
        CancellationToken ct
    )
    {
            var query = new BrowseBooksQuery(bookStoreId);
            var result = await queryDispatcher.QueryAsync(query, ct);

            return result is null
                ? TypedResults.NotFound()
                : TypedResults.Ok(result);
    }
}