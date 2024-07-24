
namespace DotNetBoilerplate.Core.Books;

public interface IBookRepository
{
    Task AddAsync(Book book);

    Task<bool> UserCanNotAddBookAsync(Guid bookStoreId);

    Task<Book> GetByIdAsync(Guid id);

    Task UpdateAsync(Book book);

    Task<IEnumerable<Book>> GetAll();

    
}