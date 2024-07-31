using DotNetBoilerplate.Application.Books.DTO;
using DotNetBoilerplate.Application.Books.Browse;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace DotNetBoilerplate.Infrastructure.DAL.Handlers.Books;

internal sealed class BrowseBooksHandler(
    DotNetBoilerplateReadDbContext dbContext
) : IQueryHandler<BrowseBooksQuery, IEnumerable<BookDto>>
{
    public async Task<IEnumerable<BookDto>> HandleAsync(BrowseBooksQuery query)
    {
        var booksQuery = dbContext.Books.AsNoTracking();
        if (query.BookStoreId.HasValue)
            booksQuery = booksQuery.Where(x => x.BookStoreId == query.BookStoreId.Value);

        return await booksQuery
            .Select(x=>new BookDto(
                x.Id,
                x.Title,
                x.Writer,
                x.Genre,
                x.Year,
                x.Description
            ))
            .ToListAsync();
    }
}