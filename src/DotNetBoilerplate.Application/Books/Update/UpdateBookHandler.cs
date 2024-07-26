using DotNetBoilerplate.Application.Exceptions;
using DotNetBoilerplate.Core.Books;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Contexts;

namespace DotNetBoilerplate.Application.Books.Update;

internal sealed class UpdateBookHandler(
    IContext context,
    IBookRepository bookRepository
) : ICommandHandler<UpdateBookCommand, Guid>
{
    public async Task<Guid> HandleAsync(UpdateBookCommand command)
    {
        var book = await bookRepository.GetByIdAsync(command.Id);
        if (book is null)
            throw new BookNotFoundException();

        book.Update(
            command.Title,
            command.Writer,
            command.Genre,
            command.Year,
            command.Description,
            context.Identity.Id
        );

        await bookRepository.UpdateAsync(book);
        return book.Id;
    }
}
