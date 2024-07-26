using DotNetBoilerplate.Core.Books;
using DotNetBoilerplate.Core.BookStores;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Application.Exceptions;

namespace DotNetBoilerplate.Application.Books.Delete;

internal sealed class DeleteBookHandler(
    IContext context,
    IBookRepository bookRepository
    ) : ICommandHandler<DeleteBookCommand>
{
    public async Task HandleAsync(DeleteBookCommand command)
    {
        var book = await bookRepository.GetByIdAsync(command.bookId);

        if (book is null)
            throw new BookNotFoundException();
        if (book.CreatedBy != context.Identity.Id)
            throw new UserCanNotDeleteBookException();

        await bookRepository.DeleteAsync(book);
    }
}
