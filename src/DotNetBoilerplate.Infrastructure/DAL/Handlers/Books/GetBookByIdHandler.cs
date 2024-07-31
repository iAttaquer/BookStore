using DotNetBoilerplate.Application.Books.DTO;
using DotNetBoilerplate.Core.Books;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using DotNetBoilerplate.Application.Exceptions;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
namespace DotNetBoilerplate.Application.Books.Get;

internal sealed class GetBookStoreByIdHandler(
    DotNetBoilerplateReadDbContext dbContext
) : IQueryHandler<GetBookByIdQuery, BookDto>
{
    public async Task<BookDto> HandleAsync(GetBookByIdQuery query)
    {
        return await dbContext.Books
            .AsNoTracking()
            .Where(x => x.Id == query.Id)
            .Include(x=>x.Reviews)
            .Select(x=>new BookDto(
                x.Id,
                x.Title,
                x.Writer,
                x.Genre,
                x.Year,
                x.Description
            ))
            .FirstOrDefaultAsync();
    }
}