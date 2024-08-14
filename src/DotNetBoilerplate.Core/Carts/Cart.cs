using DotNetBoilerplate.Core.Carts.Exceptions;
using DotNetBoilerplate.Core.Users;

namespace DotNetBoilerplate.Core.Carts; 

public class Quantity
{
    private Quantity(){}
    
    public Quantity(int value){
        if(value < 1) throw new IncorrectQuantityOfBooksException();
        Value = value;
    }
    
    public int Value { get; private set;  }
}

public class CartItem
{
    private CartItem(){}

    public CartItem(Guid bookId, Quantity quantity, Guid cartId){
        Id = Guid.NewGuid();
        BookId = bookId;
        Quantity = quantity;
        CartId = cartId;
    }

    public Guid Id { get; private set; }
    public Guid BookId { get; private set; }
    public Quantity Quantity { get; private set; }
    public Guid CartId { get; private set; }

    public void UpdateItem(Quantity quantity){
        Quantity = quantity;
    }
}

public class Cart
{
    private Cart(){}

    public Guid Id { get; private set; }
    public List<CartItem> Items { get; private set; }
    public UserId Owner { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public static Cart Create(
        UserId owner,
        DateTime createdAt,
        bool cartAlreadyExists
    ){
        if(cartAlreadyExists) throw new CartAlreadyExistsException();
        
        return new Cart
        {
            Id = Guid.NewGuid(),
            Owner = owner,
            CreatedAt = createdAt,
            Items = new List<CartItem>()
        };
    }
}
