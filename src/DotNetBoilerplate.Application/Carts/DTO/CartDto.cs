using DotNetBoilerplate.Core.Carts;
using DotNetBoilerplate.Core.Users;

namespace DotNetBoilerplate.Application.Carts.DTO;

public sealed record CartDto(
    Guid Id,
    List<CartItem> Items,
    UserId Owner,
    DateTime CreatedAt
);