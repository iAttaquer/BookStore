

namespace DotNetBoilerplate.Core.Catalogs;

public interface ICatalogRepository
{
    Task AddAsync(Catalog catalog);

    Task<bool> UserCanNotAddCatalogAsync(Guid bookStoreId);

    Task UpdateAsync(Catalog catalog);

    Task<IEnumerable<Catalog>> GetAll();

    Task<Catalog> GetByIdAsync(Guid id);

    Task<IEnumerable<Catalog>> GetAllInBookStore(Guid bookStoreId);

    Task DeleteAsync(Catalog catalog);
}
