using DotNetBoilerplate.Application.Carts.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Carts.Get;

public sealed record GetCartByIdQuery(Guid? Id = null) : IQuery<CartDto>;