using DotNetBoilerplate.Application.Carts.DTO;
using DotNetBoilerplate.Application.Carts.Get;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Carts;

public class GetCartByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("", Handle)
            .RequireAuthorization()
            .WithSummary("Get cart for owner or by id");
    }

    private static async Task<Results<Ok<CartDto>, NotFound>> Handle(
        [FromQuery] Guid? id,
        [FromServices] IQueryDispatcher queryDispatcher,
        CancellationToken ct
    )
    {
        var query = new GetCartByIdQuery(id);
        var result = await queryDispatcher.QueryAsync(query, ct);

        return result is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(result);
    }
}
