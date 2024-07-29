using DotNetBoilerplate.Application.Books.Browse;
using DotNetBoilerplate.Application.Books.DTO;
using DotNetBoilerplate.Application.Books.Get;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Books;

public class GetBooksEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("", Handle)
            .WithSummary("Browse books with optional Id or BookStoreId parameter");
    }

    private static async Task<Results<Ok<IEnumerable<BookDto>>, Ok<BookDto>, NotFound>> Handle(
        [FromQuery] Guid? id,
        [FromQuery] Guid? bookStoreId,
        [FromServices] IQueryDispatcher queryDispatcher,
        CancellationToken ct
    )
    {
        if (id.HasValue)
        {
            var query = new GetBookByIdQuery(id.Value);
            var result = await queryDispatcher.QueryAsync(query, ct);

            if (result is null) return TypedResults.NotFound();
            return TypedResults.Ok(result);
        }
        else if (bookStoreId.HasValue)
        {
            var query = new BrowseBooksByBookStoreIdQuery(bookStoreId.Value);
            var result = await queryDispatcher.QueryAsync(query, ct);

            if (result is null) return TypedResults.NotFound();
            return TypedResults.Ok(result);
        }
        else
        {
            var query = new BrowseBooksQuery();
            var result = await queryDispatcher.QueryAsync(query, ct);
            return TypedResults.Ok(result);
        }
    }
}