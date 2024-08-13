using DotNetBoilerplate.Core.Catalogs;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Application.Exceptions;
using DotNetBoilerplate.Core.Books;

namespace DotNetBoilerplate.Application.Catalogs.RemoveBook;

internal sealed class RemoveBookFromCatalogHandler(
    IContext context,
    ICatalogRepository catalogRepository,
    IBookRepository bookRepository
    ) : ICommandHandler<RemoveBookFromCatalogCommand>
{
    public async Task HandleAsync(RemoveBookFromCatalogCommand command)
    {
        var catalog = await catalogRepository.GetByIdAsync(command.CatalogId);
        if (catalog is null)
            throw new CatalogNotFoundException();

        var book = await bookRepository.GetByIdAsync(command.BookId);
        if (book is null)
            throw new BookNotFoundException();

        if (catalog.CreatedBy != context.Identity.Id)
            throw new UserCanNotRemoveBookFromCatalogException();

        await catalogRepository.RemoveBookFromCatalogAsync(book, catalog);
    }
}
