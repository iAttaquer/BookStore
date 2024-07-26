using DotNetBoilerplate.Core.Catalogs;

namespace DotNetBoilerplate.Infrastructure.DAL.Repositories;

internal sealed class InMemoryCatalogRepository : ICatalogRepository
{
    private readonly List<Catalog> _catalogs = [];

    public Task AddAsync(Catalog catalog)
    {
        _catalogs.Add(catalog);
        return Task.CompletedTask;
    }
}
