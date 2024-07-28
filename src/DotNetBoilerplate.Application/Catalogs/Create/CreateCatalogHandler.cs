using DotNetBoilerplate.Core.Catalogs;
using DotNetBoilerplate.Core.BookStores;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Application.Exceptions;
using DotNetBoilerplate.Shared.Abstractions.Contexts;

namespace DotNetBoilerplate.Application.Catalogs.Create;

internal sealed class CreateCatalogHandler(
    IContext context,
    ICatalogRepository catalogRepository,
    IBookStoreRepository bookStoreRepository
) : ICommandHandler<CreateCatalogCommand, Guid>
{
    public async Task<Guid> HandleAsync(CreateCatalogCommand command)
    {
        var bookStore = await bookStoreRepository.GetByOwnerIdAsync(context.Identity.Id);
        if (bookStore is null)
            throw new BookStoreNotFoundException();

        bool userCanNotCreateCatalog = await catalogRepository.UserCanNotAddCatalogAsync(bookStore.Id);
        
        var catalog = Catalog.Create(
            command.Name,
            command.Genre,
            command.Description,
            bookStore.Id,
            context.Identity.Id,
            userCanNotCreateCatalog
        );
        await catalogRepository.AddAsync(catalog);
        return catalog.Id;
    }
}