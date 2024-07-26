using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Catalogs.Create;

public sealed record CreateCatalogCommand(string Name, string Genre, string Description) : ICommand<Guid>;