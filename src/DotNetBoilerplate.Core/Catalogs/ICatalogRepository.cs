using DotNetBoilerplate.Core.Books;

namespace DotNetBoilerplate.Core.Catalogs;

public interface ICatalogRepository
{
    Task AddAsync(Catalog catalog);

    Task<bool> UserCanNotAddCatalogAsync(Guid bookStoreId);

    Task UpdateAsync(Catalog catalog);

    Task<bool> UserCanNotUpdateCatalogAsync(DateTime lastUpdated, DateTime now);

    Task<IEnumerable<Catalog>> GetAll();

    Task<Catalog> GetByIdAsync(Guid id);

    Task<IEnumerable<Catalog>> GetAllInBookStore(Guid bookStoreId);

    Task DeleteAsync(Catalog catalog);

    Task AddBookToCatalogAsync(Book book, Catalog catalog);

    Task<IEnumerable<Book>> GetBooksInCatalogAsync(Guid catalogId);

    Task RemoveBookFromCatalogAsync(Book book, Catalog catalog);
}
