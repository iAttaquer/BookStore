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

    public Task UpdateAsync(Catalog catalog)
    {
        var existingCatalogIndex = _catalogs.FindIndex(x => x.Id == catalog.Id);
        _catalogs[existingCatalogIndex] = catalog;

        return Task.CompletedTask;
    }

    public Task<IEnumerable<Catalog>> GetAll()
    {
        return Task.FromResult(_catalogs.AsEnumerable());
    }

    public Task<Catalog> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_catalogs.FirstOrDefault(x => x.Id == id));
    }

    public Task<IEnumerable<Catalog>> GetAllInBookStore(Guid bookStoreId)
    {
        return Task.FromResult(_catalogs.FindAll(x => x.BookStoreId == bookStoreId)
           .AsEnumerable());
    }

    public Task DeleteAsync(Catalog catalog)
    {
        _catalogs.Remove(catalog);
        return Task.CompletedTask;
    }
}
