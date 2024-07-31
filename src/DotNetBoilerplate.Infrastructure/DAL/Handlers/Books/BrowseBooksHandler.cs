using DotNetBoilerplate.Application.Books.DTO;
using DotNetBoilerplate.Core.Books;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace DotNetBoilerplate.Application.Books.Browse;

internal sealed class BrowseBooksHandler(
    DotNetBoilerplateReadDbContext dbContext
) : IQueryHandler<BrowseBooksQuery, IEnumerable<BookDto>>
{
    public async Task<IEnumerable<BookDto>> HandleAsync(BrowseBooksQuery query)
    {
        return await dbContext.Books
            .AsNoTracking()
            .Include(x=>x.Reviews)
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