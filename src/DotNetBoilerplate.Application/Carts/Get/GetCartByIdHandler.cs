using DotNetBoilerplate.Application.Carts.DTO;
using DotNetBoilerplate.Core.Carts;
using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Queries;
namespace DotNetBoilerplate.Application.Carts.Get;

internal sealed class GetCartByIdHandler(
    IContext context,
    ICartRepository cartRepository
) : IQueryHandler<GetCartByIdQuery, CartDto>
{
    public async Task<CartDto> HandleAsync(GetCartByIdQuery query)
    {     
        var cart = query.Id.HasValue 
            ? await cartRepository.GetByIdAsync(query.Id.Value) 
            : await cartRepository.GetCartByOwnerIdAsync(context.Identity.Id);
        
        if (cart is null)
            return null;

        if(cart.Owner != context.Identity.Id)
            return null;

        return new CartDto(
                cart.Id,
                cart.Items,
                cart.Owner,
                cart.CreatedAt
            );
    }
}