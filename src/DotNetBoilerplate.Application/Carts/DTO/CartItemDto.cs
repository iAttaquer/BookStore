using DotNetBoilerplate.Core.Carts;
using DotNetBoilerplate.Core.Users;

namespace DotNetBoilerplate.Application.Carts.DTO;

public sealed record CartItemDto(
    Guid Id,
    Guid BookId,
    int Quantity,
    Guid CartId
);