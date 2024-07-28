using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Catalogs.Update;

public sealed record UpdateCatalogCommand(Guid Id, string Name, string Genre, string Description) : ICommand<Guid>;