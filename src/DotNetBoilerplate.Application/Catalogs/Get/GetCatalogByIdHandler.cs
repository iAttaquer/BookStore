using DotNetBoilerplate.Application.Catalogs.DTO;
using DotNetBoilerplate.Core.Catalogs;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using DotNetBoilerplate.Application.Exceptions;
namespace DotNetBoilerplate.Application.Catalogs.Get;

internal sealed class GetBookStoreByIdHandler(
    ICatalogRepository catalogRepository
) : IQueryHandler<GetCatalogByIdQuery, CatalogDto>
{
    public async Task<CatalogDto> HandleAsync(GetCatalogByIdQuery query)
    {
        var catalog = await catalogRepository.GetByIdAsync(query.Id);
        if (catalog is null)
            return null;

        return new CatalogDto(
                catalog.Id,
                catalog.Name,
                catalog.Genre,
                catalog.Description
            );
    }
}