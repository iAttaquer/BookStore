using DotNetBoilerplate.Application.Exceptions;
using DotNetBoilerplate.Core.Catalogs;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Contexts;

namespace DotNetBoilerplate.Application.Catalogs.Update;

internal sealed class UpdateCatalogHandler(
    IContext context,
    ICatalogRepository catalogRepository
) : ICommandHandler<UpdateCatalogCommand, Guid>
{
    public async Task<Guid> HandleAsync(UpdateCatalogCommand command)
    {
        var catalog = await catalogRepository.GetByIdAsync(command.Id);
        if (catalog is null)
            throw new BookNotFoundException();

        catalog.Update(
            command.Name,
            command.Genre,
            command.Description,
            context.Identity.Id
        );

        await catalogRepository.UpdateAsync(catalog);
        return catalog.Id;
    }
}
