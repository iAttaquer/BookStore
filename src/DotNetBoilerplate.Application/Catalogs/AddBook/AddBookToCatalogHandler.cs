using DotNetBoilerplate.Core.Catalogs;
using DotNetBoilerplate.Core.Books;
using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Catalogs.AddBook;

internal sealed class AddBookToCatalogHandler(
    ICatalogRepository catalogRepository,
    IBookRepository bookRepository
) : ICommandHandler<AddBookToCatalogCommand, Guid>
{
    public async Task<Guid> HandleAsync(AddBookToCatalogCommand command)
    {
        var catalog = await catalogRepository.GetByIdAsync(command.CatalogId);
        var book = await bookRepository.GetByIdAsync(command.BookId);

        await catalogRepository.AddBookToCatalogAsync(book, catalog);

        return catalog.Id;
    }
}