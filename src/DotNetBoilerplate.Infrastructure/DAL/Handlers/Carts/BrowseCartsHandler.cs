using DotNetBoilerplate.Application.Carts.DTO;
using DotNetBoilerplate.Core.Carts;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace DotNetBoilerplate.Application.Carts.Browse;

internal sealed class BrowseCartsHandler(
    DotNetBoilerplateReadDbContext dbContext
) : IQueryHandler<BrowseCartsQuery, IEnumerable<CartDto>>
{
    public async Task<IEnumerable<CartDto>> HandleAsync(BrowseCartsQuery query)
    {
        var cartsQuery = dbContext.Carts
            .Include(x=>x.Items)
            .AsNoTracking();

        return await cartsQuery
        .Select(
            x => new CartDto(
                x.Id, 
                x.Items.Select(item => new CartItemDto(
                    item.Id, 
                    item.BookId, 
                    item.Quantity, 
                    item.CartId))
                    .ToList(),
                x.Owner, 
                x.CreatedAt
            ))
        .ToListAsync();
    }
}