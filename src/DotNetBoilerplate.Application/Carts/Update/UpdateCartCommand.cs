using DotNetBoilerplate.Core.Carts;
using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Carts.Update;

public sealed record UpdateCartCommand(Guid BookId, Quantity Quantity) : ICommand<Guid>;
