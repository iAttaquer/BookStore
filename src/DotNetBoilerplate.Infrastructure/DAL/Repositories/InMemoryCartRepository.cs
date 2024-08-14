using DotNetBoilerplate.Core.Carts;
using DotNetBoilerplate.Core.Users;

namespace DotNetBoilerplate.Infrastructure.DAL.Repositories;

internal sealed class InMemoryCartRepository : ICartRepository
{
    private readonly List<Cart> _carts = [];

    public Task AddCartAsync(Cart cart)
    {
        _carts.Add(cart);
        return Task.CompletedTask;
    }

    public Task<bool> CartAlreadyExistsAsync(UserId ownerId)
    {
        return Task.FromResult(_carts.Any(x=>x.Owner==ownerId));
    }

    public Task UpdateCartAsync(Cart cart, CartItem item)
    {
        var existingItem = cart.Items.FirstOrDefault(x=>x.BookId == item.BookId);

        if(existingItem is null) cart.Items.Add(item);
        else existingItem.UpdateItem(item.Quantity);
        
        return Task.CompletedTask;
    }

    public Task<Cart> GetCartByOwnerIdAsync(UserId ownerId)
    {
        return Task.FromResult(_carts.FirstOrDefault(x => x.Owner == ownerId));
    }

    public Task<Cart> GetByIdAsync(Guid cartId)
    {
        return Task.FromResult(_carts.FirstOrDefault(x => x.Id == cartId));
    }

    public Task<IEnumerable<Cart>> GetAllAsync()
    {
        return Task.FromResult(_carts.AsEnumerable());
    }

    public Task DeleteAsync(Cart cart, CartItem item)
    {
        if(item is null)
        {
            _carts.Remove(cart);
        }
        else
        {
           var existingCart = _carts.FindIndex(x=>x.Id==cart.Id);
           _carts[existingCart].Items.Remove(item);
        }
        
        return Task.CompletedTask;
    }
}