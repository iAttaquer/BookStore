using DotNetBoilerplate.Application.Carts.DTO;
using DotNetBoilerplate.Core.Carts;
using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;
namespace DotNetBoilerplate.Application.Carts.Get;

internal sealed class GetCartByIdHandler(
    IContext context,
    DotNetBoilerplateReadDbContext dbContext
) : IQueryHandler<GetCartByIdQuery, CartDto>
{
    public async Task<CartDto> HandleAsync(GetCartByIdQuery query)
    {     
        var cartQuery = dbContext.Carts
            .Include(x=>x.Items)
            .AsNoTracking();
        if(query.Id.HasValue) cartQuery = cartQuery.Where(x=>x.Id==query.Id);
        else cartQuery = cartQuery.Where(x=>x.Owner == context.Identity.Id);

        return await cartQuery
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
        .FirstOrDefaultAsync();
    }
}