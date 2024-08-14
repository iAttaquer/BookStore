using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Catalogs.RemoveBook;

public sealed record RemoveBookFromCatalogCommand(Guid CatalogId, Guid BookId) : ICommand<Guid>;
