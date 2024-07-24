using DotNetBoilerplate.Core.Books;
using DotNetBoilerplate.Core.BookStores;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Contexts;

namespace DotNetBoilerplate.Application.Books.Create;

internal sealed class CreateBookHandler(
    IContext context,
    IBookRepository bookRepository,
    IBookStoreRepository bookStoreRepository
    ) : ICommandHandler<CreateBookCommand, Guid>
{
    public async Task<Guid> HandleAsync(CreateBookCommand command )
    {
        var bookStore = await bookStoreRepository.GetByOwnerIdAsync(context.Identity.Id);
        var userCanNotAddBook = await bookRepository.UserCanNotAddBookAsync(bookStore.Id);

        var book = Book.Create(
            command.Title,
            command.Writer,
            command.Genre,
            command.Year,
            command.Description,
            bookStore.Id,
            userCanNotAddBook
            );
        await bookRepository.AddAsync(book);
        return book.Id;
    }
}
