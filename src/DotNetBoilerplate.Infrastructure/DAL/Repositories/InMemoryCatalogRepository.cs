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

    public Task<bool> UserCanNotAddCatalogAsync(Guid bookStoreId)
    {
        var count = _catalogs.Count(x => x.BookStoreId == bookStoreId);
        return Task.FromResult(count >= 5);
    }
}
