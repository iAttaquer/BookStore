using DotNetBoilerplate.Core.Books;
using DotNetBoilerplate.Core.Catalogs.Exceptions;

namespace DotNetBoilerplate.Core.Catalogs;

public sealed class Catalog
{
    private Catalog()
    { }
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Genre { get; private set; }
    public string Description { get; private set; }
    public List<Book> Books { get; private set; }
    public Guid BookStoreId { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public static Catalog Create(
        string name,
        string genre,
        string description,
        Guid bookStoreId,
        Guid createdBy,
        DateTime updatedAt,
        bool userCanNotCreateCatalog
        )
    {
        if (userCanNotCreateCatalog)
            throw new UserCanNotCreateCatalogException();

        return new Catalog
        {
            Id = Guid.NewGuid(),
            Name = name,
            Genre = genre,
            Description = description,
            BookStoreId = bookStoreId,
            CreatedBy = createdBy,
            UpdatedAt = updatedAt
        };
    }
    public void Update(
        string name,
        string genre,
        string description,
        Guid updater,
        DateTime updatedAt,
        bool userCanNotUpdateCatalog

    )
    {
        if (updater == Guid.Empty)
            throw new UserCanNotUpdateCatalogException();
        if (userCanNotUpdateCatalog)
            throw new UserCanNotUpdateCatalogMoreThanOnceIn3DaysException();

        Name = name;
        Genre = genre;
        Description = description;
        UpdatedAt = updatedAt;
    }
}
