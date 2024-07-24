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
            .WithSummary("Browse all books");
    }

    private static async Task<Ok<IEnumerable<BookDto>>> Handle(
        [FromServices] IQueryDispatcher queryDispatcher,
        CancellationToken ct
    )
    {
        var query = new BrowseBooksQuery();

        var result = await queryDispatcher.QueryAsync(query, ct);

        return TypedResults.Ok(result);
    }
}