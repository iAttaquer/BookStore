using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.BookStores.Update;

public sealed record UpdateBookStoreCommand(Guid Id, string Name, string Description) : ICommand<Guid>;

