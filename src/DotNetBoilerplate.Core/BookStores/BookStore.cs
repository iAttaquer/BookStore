using DotNetBoilerplate.Core.BookStores.Exceptions;
using DotNetBoilerplate.Core.Users;

namespace DotNetBoilerplate.Core.BookStores;

public sealed class BookStore
{
    private BookStore()
    {
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }
    public string Description { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public UserId OwnerId { get; private set; }

    public static BookStore Create(
        string name,
        string description,
        DateTime createdAt,
        Guid ownerId,
        bool userAlreadyOwnsOrganization
    )
    {
        if (userAlreadyOwnsOrganization)
            throw new UserCanNotOwnMoreThanOneOrganizationException();

        return new BookStore
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            CreatedAt = createdAt,
            OwnerId = ownerId
        };
    }

    public void Update(
        string name,
        string description,
        Guid ownerId
    )
    {
        if (OwnerId != ownerId)
            throw new UserCanNotUpdateOrganizationException();

        Name = name;
        Description = description;
    }
}