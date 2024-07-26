
namespace DotNetBoilerplate.Core.Books;

public interface IBookRepository
{
    Task AddAsync(Book book);

    Task<bool> UserCanNotAddBookAsync(Guid bookStoreId);

    Task UpdateAsync(Book book);

    Task<IEnumerable<Book>> GetAll();

    Task<Book> GetByIdAsync(Guid id);

    Task<IEnumerable<Book>> GetAllInBookStore(Guid bookStoreId);

    Task DeleteAsync(Book book);
}