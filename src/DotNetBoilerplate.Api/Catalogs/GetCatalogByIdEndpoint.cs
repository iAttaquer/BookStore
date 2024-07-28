using DotNetBoilerplate.Application.Catalogs.Get;
using DotNetBoilerplate.Application.Catalogs.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Catalogs;

public class GetCatalogByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("{id:guid}", Handle)
            .WithSummary("Browse catalog by Id");
    }

    private static async Task<Results<Ok<CatalogDto>, NotFound>> Handle(
        [FromRoute] Guid id,
        [FromServices] IQueryDispatcher queryDispatcher,
        CancellationToken ct
    )
    {
        var query = new GetCatalogByIdQuery(id);

        var result = await queryDispatcher.QueryAsync(query, ct);

        if (result is null) return TypedResults.NotFound();

        return TypedResults.Ok(result);
    }
}