using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Carts.Delete;

public sealed record DeleteCommand(Guid cartId, Guid? bookId = null) : ICommand<Guid>;
