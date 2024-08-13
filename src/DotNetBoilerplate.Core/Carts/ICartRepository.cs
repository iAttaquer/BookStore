using DotNetBoilerplate.Core.Carts;
using DotNetBoilerplate.Core.Users;

namespace DotNetBoilerplate.Core.Carts;
public interface ICartRepository
{
    Task AddCartAsync(Cart cart);

    Task UpdateCartAsync(Cart cart, CartItem item);

    Task <bool> CartAlreadyExistsAsync(UserId ownerId);

    Task<Cart> GetCartByOwnerIdAsync(UserId ownerId);

    Task<Cart> GetByIdAsync(Guid cartId);

    Task<IEnumerable<Cart>> GetAllAsync();

    Task DeleteAsync(Cart cart, CartItem? item = null);
}
