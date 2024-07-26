
namespace DotNetBoilerplate.Core.Catalogs;

public sealed class Catalog
{
    private Catalog()
    { }
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Genre { get; private set; }
    public string Description { get; private set; }
    public Guid BookStoreId { get; private set; }

    public static Catalog Create(
        string name,
        string genre,
        string description,
        Guid bookStoreId
        )
    {
        return new Catalog
        {
            Id = Guid.NewGuid(),
            Name = name,
            Genre = genre,
            Description = description,
            BookStoreId = bookStoreId
        };
    }
}
