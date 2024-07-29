using DotNetBoilerplate.Core.Books;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DotNetBoilerplate.Infrastructure.DAL.Repositories;

internal sealed class PostgresBookRepository(DotNetBoilerplateWriteDbContext dbContext) : IBookRepository
{
    public async Task AddAsync(Book book)
    {
        await dbContext.Books.AddAsync(book);
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> UserCanNotAddBookAsync(Guid bookStoreId)
    {
        return await dbContext.Books.CountAsync(x => x.BookStoreId == bookStoreId) >= 100;
    }

    public async Task UpdateAsync(Book book)
    {
        dbContext.Books.Update(book);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Book>> GetAll()
    {
        return await dbContext.Books.ToListAsync();
    }

    public async Task<Book> GetByIdAsync(Guid id)
    {
        return await dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Book>> GetAllInBookStore(Guid bookStoreId)
    {
        return await dbContext.Books.Where(x => x.BookStoreId == bookStoreId).ToListAsync();
    }

    public async Task DeleteAsync(Book book)
    {
        dbContext.Books.Remove(book);
        await dbContext.SaveChangesAsync();
    }
}