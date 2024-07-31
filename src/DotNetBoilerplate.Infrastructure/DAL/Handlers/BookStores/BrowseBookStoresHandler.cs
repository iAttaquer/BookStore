using DotNetBoilerplate.Application.BookStores.DTO;
using DotNetBoilerplate.Application.BookStores.Browse;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DotNetBoilerplate.Infrastructure.DAL.Handlers.BookStores;

internal sealed class BrowseBookStoresHandler(
    DotNetBoilerplateReadDbContext dbContext
) : IQueryHandler<BrowseBookStoresQuery, IEnumerable<BookStoreDto>>
{
    public async Task<IEnumerable<BookStoreDto>> HandleAsync(BrowseBookStoresQuery query)
    {
        return await dbContext.BookStores
            .AsNoTracking()
            .Select(x => new BookStoreDto(
                x.Id,
                x.Name,
                x.Description,
                x.OwnerId
            ))
            .ToListAsync();
    }
}