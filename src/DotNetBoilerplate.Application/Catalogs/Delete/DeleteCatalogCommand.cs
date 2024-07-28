using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Catalogs.Delete;

public sealed record DeleteCatalogCommand(Guid Id) : ICommand<Guid>;
