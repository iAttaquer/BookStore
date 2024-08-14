using DotNetBoilerplate.Core.Catalogs;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Application.Exceptions;

namespace DotNetBoilerplate.Application.Catalogs.Delete;

internal sealed class DeleteBookHandler(
    IContext context,
    ICatalogRepository catalogRepository
    ) : ICommandHandler<DeleteCatalogCommand>
{
    public async Task HandleAsync(DeleteCatalogCommand command)
    {
        var catalog = await catalogRepository.GetByIdAsync(command.Id);

        if (catalog is null)
            throw new CatalogNotFoundException();
        if (catalog.CreatedBy != context.Identity.Id)
            throw new UserCanNotDeleteBookException();

        await catalogRepository.DeleteAsync(catalog);
    }
}
