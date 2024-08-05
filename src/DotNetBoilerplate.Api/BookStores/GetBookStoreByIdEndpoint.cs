using DotNetBoilerplate.Application.BookStores.Get;
using DotNetBoilerplate.Application.BookStores.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.BookStores;

public class GetBookStoresEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("{id:guid}", Handle)
            .WithSummary("Get book store by Id");
    }

    private static async Task<Results<Ok<BookStoreDto>, NotFound>> Handle(
        [FromRoute] Guid id,
        [FromServices] IQueryDispatcher queryDispatcher,
        CancellationToken ct
    )
    {
        var query = new GetBookStoreByIdQuery(id);

        var result = await queryDispatcher.QueryAsync(query, ct);

        return result is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(result);
    }
}