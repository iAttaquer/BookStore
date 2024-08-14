using DotNetBoilerplate.Application.Catalogs.DTO;
using DotNetBoilerplate.Core.Catalogs;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Catalogs.Browse;

internal sealed class BrowseCatalogsHandler(
    ICatalogRepository catalogRepository
) : IQueryHandler<BrowseCatalogsQuery, IEnumerable<CatalogDto>>
{
    public async Task<IEnumerable<CatalogDto>> HandleAsync(BrowseCatalogsQuery query)
    {
        IEnumerable<Catalog> catalogs;
        if (query.BookStoreId.HasValue)
        {
            catalogs = await catalogRepository.GetAllInBookStore(query.BookStoreId.Value);
            return catalogs.Select(x => new CatalogDto(x.Id, x.Name, x.Genre, x.Description));
        }
        catalogs = await catalogRepository.GetAll();
        return catalogs.Select(x => new CatalogDto(x.Id, x.Name, x.Genre, x.Description));
    }
}