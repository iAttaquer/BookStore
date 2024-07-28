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
        var books = await catalogRepository.GetAll();

        return books
            .Select(x => new CatalogDto(x.Id, x.Name, x.Genre, x.Description))
            .ToList();
    }
}