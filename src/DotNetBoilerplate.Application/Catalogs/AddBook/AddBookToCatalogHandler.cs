using DotNetBoilerplate.Core.Catalogs;
using DotNetBoilerplate.Core.Books;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Application.Exceptions;

namespace DotNetBoilerplate.Application.Catalogs.AddBook;

internal sealed class AddBookToCatalogHandler(
    ICatalogRepository catalogRepository,
    IBookRepository bookRepository
) : ICommandHandler<AddBookToCatalogCommand, Guid>
{
    public async Task<Guid> HandleAsync(AddBookToCatalogCommand command)
    {
        var catalog = await catalogRepository.GetByIdAsync(command.CatalogId);
        if (catalog is null)
            throw new CatalogNotFoundException();
        var book = await bookRepository.GetByIdAsync(command.BookId);
        if (book is null)
            throw new BookNotFoundException();

        await catalogRepository.AddBookToCatalogAsync(book, catalog);

        return catalog.Id;
    }
}