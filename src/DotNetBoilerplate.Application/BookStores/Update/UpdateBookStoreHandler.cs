using DotNetBoilerplate.Application.Exceptions;
using DotNetBoilerplate.Core.BookStores;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Contexts;

namespace DotNetBoilerplate.Application.BookStores.Update;

internal sealed class UpdateBookStoreHandler(
    IContext context,
    IBookStoreRepository bookStoreRepository
) : ICommandHandler<UpdateBookStoreCommand,Guid>
{
    public async Task<Guid> HandleAsync(UpdateBookStoreCommand command)
    {
        var bookStore = await bookStoreRepository.GetByIdAsync(command.Id);
        if (bookStore is null)
            throw new BookStoreNotFoundException();
        
        bookStore.Update(
            command.Name,
            command.Description,
            context.Identity.Id
        );

        await bookStoreRepository.UpdateAsync(bookStore);
        return bookStore.Id;
    }
}
