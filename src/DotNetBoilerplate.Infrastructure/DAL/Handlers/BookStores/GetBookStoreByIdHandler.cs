using DotNetBoilerplate.Application.BookStores.DTO;
using DotNetBoilerplate.Application.BookStores.Get;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using DotNetBoilerplate.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DotNetBoilerplate.Infrastructure.DAL.Handlers.BookStores;

internal sealed class GetBookStoreByIdHandler(
    DotNetBoilerplateReadDbContext dbContext
) : IQueryHandler<GetBookStoreByIdQuery, BookStoreDto>
{
    public async Task<BookStoreDto> HandleAsync(GetBookStoreByIdQuery query)
    {
        return await dbContext.BookStores
            .AsNoTracking()
            .Where(x => x.Id == query.Id)
            .Select(x => new BookStoreDto(
                x.Id,
                x.Name,
                x.Description,
                x.OwnerId
            ))
            .FirstOrDefaultAsync();
    }
}