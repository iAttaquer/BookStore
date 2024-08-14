using DotNetBoilerplate.Application.Carts.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Carts.Browse;

public sealed record BrowseCartsQuery() : IQuery<IEnumerable<CartDto>>;