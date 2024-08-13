namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Read.Model;

internal sealed class CartItemReadModel
{
    public Guid Id { get; set; }

    public Guid BookId { get; set; }
    public BookReadModel Book { get; set; }

    public int Quantity { get; set; }

    public Guid CartId { get; set; }
    public CartReadModel Cart { get; set; }
}