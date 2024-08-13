
using DotNetBoilerplate.Core.Books;
using DotNetBoilerplate.Core.Catalogs;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DotNetBoilerplate.Infrastructure.DAL.Repositories;

internal sealed class PostgresCatalogRepository(DotNetBoilerplateWriteDbContext dbContext) : ICatalogRepository
{
    public async Task AddAsync(Catalog catalog)
    {
        await dbContext.Catalogs.AddAsync(catalog);
    }
    public async Task<bool> UserCanNotAddCatalogAsync(Guid bookStoreId)
    {
        return await dbContext.Catalogs.CountAsync(x => x.BookStoreId == bookStoreId) >= 5;
    }
    public async Task UpdateAsync(Catalog catalog)
    {
        dbContext.Catalogs.Update(catalog);
        await dbContext.SaveChangesAsync();
    }
    public Task<bool> UserCanNotUpdateCatalogAsync(DateTime lastUpdated, DateTime now)
    {
        return Task.FromResult((now - lastUpdated).TotalSeconds < 3);
    }
    public async Task<IEnumerable<Catalog>> GetAll()
    {
        return await dbContext.Catalogs.ToListAsync();
    }
    public async Task<Catalog> GetByIdAsync(Guid id)
    {
        return await dbContext.Catalogs.FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<IEnumerable<Catalog>> GetAllInBookStore(Guid bookStoreId)
    {
        return await dbContext.Catalogs.Where(x => x.BookStoreId == bookStoreId).ToListAsync();
    }
    public async Task DeleteAsync(Catalog catalog)
    {
        dbContext.Catalogs.Remove(catalog);
        await dbContext.SaveChangesAsync();
    }


    public async Task AddBookToCatalogAsync(Book book, Catalog catalog)
    {
        var existingCatalog = await dbContext.Catalogs.Include(c => c.Books).FirstOrDefaultAsync(x => x.Id == catalog.Id);
        // if (existingCatalog.Books == null)
        // {
        //     existingCatalog.Books = new List<Book>();
        // }
        existingCatalog.Books.Add(book);
        await dbContext.SaveChangesAsync();
    }
    public async Task<IEnumerable<Book>> GetBooksInCatalogAsync(Guid catalogId)
    {
        var catalog = await dbContext.Catalogs
            .Include(c => c.Books)
            .FirstOrDefaultAsync(x => x.Id == catalogId);
        return catalog.Books.ToList();
    }
    public async Task RemoveBookFromCatalogAsync(Book book, Catalog catalog)
    {
        var existingCatalog = await dbContext.Catalogs
            .Include(c => c.Books)
            .FirstOrDefaultAsync(x => x.Id == catalog.Id);
        existingCatalog.Books.Remove(book);
        await dbContext.SaveChangesAsync();
    }
}