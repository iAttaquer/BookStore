using Microsoft.EntityFrameworkCore;
using DotNetBoilerplate.Core.Carts;
using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;

namespace DotNetBoilerplate.Infrastructure.DAL.Repositories;

internal sealed class PostgresCartRepository(DotNetBoilerplateWriteDbContext dbContext) : ICartRepository
{
    public async Task AddCartAsync(Cart cart)
    {
        await dbContext.AddAsync(cart);
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> CartAlreadyExistsAsync(UserId ownerId)
    {
        return await dbContext.Carts.AnyAsync(x=>x.Owner==ownerId);
    }

    public async Task UpdateCartAsync(Cart cart, CartItem item)
    {
        var existingItem = dbContext.CartItems
                        .Where(x=>x.CartId==cart.Id)
                        .FirstOrDefault(x=>x.BookId==item.BookId);

        if(existingItem is null) dbContext.CartItems.Add(item);
        else existingItem.UpdateItem(item.Quantity);

        await dbContext.SaveChangesAsync();
    }

    public async Task<Cart> GetCartByOwnerIdAsync(UserId ownerId)
    {
        return await dbContext.Carts.Include(x=>x.Items).FirstOrDefaultAsync(x=>x.Owner==ownerId);
    }

    public async Task<Cart> GetByIdAsync(Guid cartId)
    {
        return await dbContext.Carts.Include(x=>x.Items).FirstOrDefaultAsync(x=>x.Id == cartId);
    }

    public async Task<IEnumerable<Cart>> GetAllAsync()
    {
        return await dbContext.Carts.Include(x=>x.Items).ToListAsync();
    }

    public async Task DeleteAsync(Cart cart, CartItem item)
    {
        if(item is null)
        {
            dbContext.Carts.Remove(cart);
        }
        else
        {
           var existingCart = await GetByIdAsync(cart.Id);
           var itemToRemove = existingCart.Items.FirstOrDefault(x=>x.BookId==item.BookId);

            existingCart.Items.Remove(itemToRemove);
        }
        
        await dbContext.SaveChangesAsync();
    }
     
}