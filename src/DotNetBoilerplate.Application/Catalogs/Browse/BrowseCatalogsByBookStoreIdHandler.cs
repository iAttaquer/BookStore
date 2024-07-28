using DotNetBoilerplate.Application.Catalogs.DTO;
using DotNetBoilerplate.Core.Catalogs;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Catalogs.Browse;

internal sealed class BrowseBooksByBookStoreIdHandler(
    ICatalogRepository catalogRepository
) : IQueryHandler<BrowseCatalogsByBookStoreIdQuery, IEnumerable<CatalogDto>>
{
    public async Task<IEnumerable<CatalogDto>> HandleAsync(BrowseCatalogsByBookStoreIdQuery query)
    {
        var books = await catalogRepository.GetAllInBookStore(query.Id);

        return books
            .Select(x => new CatalogDto(x.Id, x.Name, x.Genre, x.Description))
            .ToList();
    }
}