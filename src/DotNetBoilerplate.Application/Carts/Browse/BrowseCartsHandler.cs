using DotNetBoilerplate.Application.Carts.DTO;
using DotNetBoilerplate.Core.Carts;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Carts.Browse;

internal sealed class BrowseCartsHandler(
    ICartRepository cartRepository
) : IQueryHandler<BrowseCartsQuery, IEnumerable<CartDto>>
{
    public async Task<IEnumerable<CartDto>> HandleAsync(BrowseCartsQuery query)
    {
        var carts = await cartRepository.GetAllAsync();

        return carts
            .Select(x => new CartDto(x.Id, x.Items, x.Owner, x.CreatedAt))
            .ToList();
    }
}