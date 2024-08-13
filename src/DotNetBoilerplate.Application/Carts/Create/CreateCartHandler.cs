using DotNetBoilerplate.Core.Carts;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Application.Exceptions;
using DotNetBoilerplate.Shared.Abstractions.Time;

namespace DotNetBoilerplate.Application.Carts.Create;

internal sealed class CreateCartHandler(
    IClock clock,
    IContext context,
    ICartRepository cartRepository
    ) : ICommandHandler<CreateCartCommand, Guid>
{
    public async Task<Guid> HandleAsync(CreateCartCommand command)
    {
        var cartAlreadyExists = await cartRepository.CartAlreadyExistsAsync(context.Identity.Id);

        var cart = Cart.Create(
            context.Identity.Id,
            clock.Now(),
            cartAlreadyExists
        );

        await cartRepository.AddCartAsync(cart);
        return cart.Id;
    }
}
