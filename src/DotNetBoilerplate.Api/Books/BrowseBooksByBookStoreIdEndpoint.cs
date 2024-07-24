using DotNetBoilerplate.Application.Books.Browse;
using DotNetBoilerplate.Application.Books.DTO;
using DotNetBoilerplate.Application.Books.Get;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Books;

public class BrowseBooksByBookStoreIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("bookstore/{id:guid}", Handle)
            .WithSummary("Browse all books in Book Store");
    }

    private static async Task<Results<Ok<IEnumerable<BookDto>>, NotFound>> Handle(
        [FromRoute] Guid id,
        [FromServices] IQueryDispatcher queryDispatcher,
        CancellationToken ct
    )
    { 
        var query = new BrowseBooksByBookStoreIdQuery(id);

        var result = await queryDispatcher.QueryAsync(query, ct);

        if (result is null) return TypedResults.NotFound();

        return TypedResults.Ok(result);
    }
}