using DotNetBoilerplate.Core.Carts;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Application.Exceptions;

namespace DotNetBoilerplate.Application.Carts.Delete;

internal sealed class DeleteHandler(
    IContext context,
    ICartRepository cartRepository
    ) : ICommandHandler<DeleteCommand>
{
    public async Task HandleAsync(DeleteCommand command)
    {
        var cart = await cartRepository.GetByIdAsync(command.cartId);

        if(cart is null)
            throw new UsersCartDoesntExistException();
        if(cart.Owner != context.Identity.Id)
            throw new UserCanNotDeleteCartException();

        if(command.bookId.HasValue)
        {
            var item = cart.Items.FirstOrDefault(x=>x.BookId == command.bookId.Value);
            if(item is null)
                throw new BookNotFoundException();

            await cartRepository.DeleteAsync(cart, item);
        }
        else await cartRepository.DeleteAsync(cart);
    }
}
