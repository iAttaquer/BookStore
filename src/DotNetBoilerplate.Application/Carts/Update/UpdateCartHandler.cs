using DotNetBoilerplate.Core.Carts;
using DotNetBoilerplate.Core.Books;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Application.Exceptions;

namespace DotNetBoilerplate.Application.Carts.Update;

internal sealed class UpdateCartHandler(
    IContext context,
    ICartRepository cartRepository,
    IBookRepository bookRepository
    ) : ICommandHandler<UpdateCartCommand, Guid>
{
    public async Task<Guid> HandleAsync(UpdateCartCommand command)
    {
        var cart = await cartRepository.GetCartByOwnerIdAsync(context.Identity.Id);
        if(cart is null) throw new UsersCartDoesntExistException();

        var book = await bookRepository.GetByIdAsync(command.BookId);
        if(book is null) throw new BookNotFoundException();

        var item = new CartItem(
            command.BookId,
            command.Quantity
        );

        await cartRepository.UpdateCartAsync(cart, item);
        return cart.Id;
    }
}
