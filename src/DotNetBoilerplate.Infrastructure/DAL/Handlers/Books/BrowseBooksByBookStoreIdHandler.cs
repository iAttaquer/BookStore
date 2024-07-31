using DotNetBoilerplate.Application.Books.DTO;
using DotNetBoilerplate.Application.Books.Browse;
using DotNetBoilerplate.Core.Books;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DotNetBoilerplate.Infrastructure.DAL.Handlers.Books;

internal sealed class BrowseBooksByBookStoreIdHandler(
    DotNetBoilerplateReadDbContext dbContext
) : IQueryHandler<BrowseBooksByBookStoreIdQuery, IEnumerable<BookDto>>
{
    public async Task<IEnumerable<BookDto>> HandleAsync(BrowseBooksByBookStoreIdQuery query)
    {
        return await dbContext.Books
            .AsNoTracking()
            .Where(x => x.BookStoreId == query.Id)
            .Select(x => new BookDto(
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