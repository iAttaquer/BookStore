namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Read.Model;

internal sealed class BookStoreReadModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid OwnerId { get; set; }
    public UserReadModel Owner { get; set; }
    public List<BookReadModel> Books { get; set; }
}