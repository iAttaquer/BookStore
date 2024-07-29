using DotNetBoilerplate.Core.Books.Exceptions;
using DotNetBoilerplate.Core.Users;
namespace DotNetBoilerplate.Core.Books;

public sealed class Book
{
    private Book()
    {

    }

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Writer { get; private set; }
    public string Genre { get; private set; }
    public int Year { get; private set; }
    public string Description { get; private set; }
    public Guid BookStoreId { get; private set; }
    public UserId CreatedBy { get; private set; }

    public static Book Create(
        string title,
        string writer,
        string genre,
        int year,
        string description,
        Guid bookStoreId,
        Guid createdBy,
        bool userCanNotAddBook
        )
    {
        if (userCanNotAddBook)
            throw new UserCanNotAddBookException();

        return new Book
        {
            Id = Guid.NewGuid(),
            Title = title,
            Writer = writer,
            Genre = genre,
            Year = year,
            Description = description,
            BookStoreId = bookStoreId,
            CreatedBy = createdBy
        };
    }
    public void Update(
        string title,
        string writer,
        string genre,
        int year,
        string description,
        Guid updater
        )
    {
        if (CreatedBy != updater)
            throw new UserCanNotUpdateBookException();
        Title = title;
        Writer = writer;
        Genre = genre;
        Year = year;
        Description = description;
    }
}
