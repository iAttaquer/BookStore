using DotNetBoilerplate.Core.Books;
using DotNetBoilerplate.Core.Users;

namespace DotNetBoilerplate.Infrastructure.DAL.Repositories;

internal sealed class InMemoryBookRepository : IBookRepository
{
    private readonly List<Book> _books = [];

    public Task AddAsync(Book book)
    {
        _books.Add(book);
        return Task.CompletedTask;
    }
    public Task<bool> UserCanNotAddBookAsync(Guid bookStoreId)
    {
        var count = _books.Count(x => x.BookStoreId == bookStoreId);
        return Task.FromResult(count >= 100);
    }

    public Task UpdateAsync(Book book)
    {
        var existingBookIndex = _books.FindIndex(x => x.Id == book.Id);
        _books[existingBookIndex] = book;

        return Task.CompletedTask;
    }

    public Task<IEnumerable<Book>> GetAll()
    {
        return Task.FromResult(_books.AsEnumerable());
    }

    public Task<Book> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_books.FirstOrDefault(x => x.Id == id));
    }

    public Task<IEnumerable<Book>> GetAllInBookStore(Guid bookStoreId)
    {
        return Task.FromResult(_books.FindAll(x => x.BookStoreId == bookStoreId)
            .AsEnumerable());
    }
}