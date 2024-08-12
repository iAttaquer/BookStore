using DotNetBoilerplate.Application.Exceptions;
using DotNetBoilerplate.Core.Catalogs;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Time;

namespace DotNetBoilerplate.Application.Catalogs.Update;

internal sealed class UpdateCatalogHandler(
    IClock clock,
    IContext context,
    ICatalogRepository catalogRepository
) : ICommandHandler<UpdateCatalogCommand, Guid>
{
    public async Task<Guid> HandleAsync(UpdateCatalogCommand command)
    {
        var catalog = await catalogRepository.GetByIdAsync(command.Id);
        if (catalog is null)
            throw new BookNotFoundException();

        bool userCanNotUpdateCatalog = await catalogRepository.UserCanNotUpdateCatalogAsync(catalog.UpdatedAt, clock.Now());

        catalog.Update(
            command.Name,
            command.Genre,
            command.Description,
            context.Identity.Id,
            clock.Now(),
            userCanNotUpdateCatalog
        );

        await catalogRepository.UpdateAsync(catalog);
        return catalog.Id;
    }
}
