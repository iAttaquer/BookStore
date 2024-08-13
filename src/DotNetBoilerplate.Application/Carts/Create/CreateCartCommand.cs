using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Carts.Create;

public sealed record CreateCartCommand() : ICommand<Guid>;
