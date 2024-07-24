using DotNetBoilerplate.Core.BookStores;
using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DotNetBoilerplate.Infrastructure.DAL.Repositories;

internal sealed class PostgresBookStoreRepository(DotNetBoilerplateWriteDbContext dbContext) : IBookStoreRepository
{
    public async Task AddAsync(BookStore bookStore)
    {
        await dbContext.AddAsync(bookStore);
        await dbContext.SaveChangesAsync();
    }

    public async Task<BookStore> GetByIdAsync(Guid id)
    {
        return await dbContext.BookStores
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(BookStore bookStore)
    {
        dbContext.Update(bookStore);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<BookStore>> GetAll()
    {
        return await dbContext.BookStores
            .ToListAsync();
    }

    public async Task<bool> UserAlreadyOwnsOrganizationAsync(UserId ownerId)
    {
        return await dbContext.BookStores
            .AnyAsync(x => x.OwnerId == ownerId);
    }
    public async Task<BookStore> GetByOwnerIdAsync(UserId ownerId)
    {
        return await dbContext.BookStores
            .FirstOrDefaultAsync(x => x.OwnerId == ownerId);
    }
}