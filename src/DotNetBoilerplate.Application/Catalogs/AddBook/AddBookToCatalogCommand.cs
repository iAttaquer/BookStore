using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Catalogs.AddBook;

public sealed record AddBookToCatalogCommand(Guid CatalogId, Guid BookId) : ICommand<Guid>;