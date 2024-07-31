using DotNetBoilerplate.Application.Books.DTO;
using DotNetBoilerplate.Application.Books.Get;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Books;

public class GetBookByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("{id:guid}", Handle)
            .WithSummary("Get book by Id");
    }

    private static async Task<Results<Ok<IEnumerable<BookDto>>, Ok<BookDto>, NotFound>> Handle(
        [FromRoute] Guid id,
        [FromServices] IQueryDispatcher queryDispatcher,
        CancellationToken ct
    )
    {
        var query = new GetBookByIdQuery(id);
        var result = await queryDispatcher.QueryAsync(query, ct);

        return result is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(result);
    }
}
