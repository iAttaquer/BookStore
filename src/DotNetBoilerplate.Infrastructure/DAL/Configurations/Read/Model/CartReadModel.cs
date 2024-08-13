namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Read.Model;

internal sealed class CartReadModel
{
    public Guid Id { get; set; }
    public List<CartItemReadModel> Items{get; set; }
    public DateTime CreatedAt { get; set; }
    public UserReadModel OwnerModel { get; set; }
    public Guid Owner { get; set; }
}