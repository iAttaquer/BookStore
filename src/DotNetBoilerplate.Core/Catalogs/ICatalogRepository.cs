

namespace DotNetBoilerplate.Core.Catalogs;

public interface ICatalogRepository
{
    Task AddAsync(Catalog catalog);

    Task<bool> UserCanNotAddCatalogAsync(Guid bookStoreId);
}
