namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Read.Model;

internal sealed class BookReadModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    public string Writer { get; set; }
    public string Genre { get; set; }
    public int Year { get; set; }
    public string Description { get; set; }

    public Guid BookStoreId { get; set; }
    public BookStoreReadModel BookStore { get; set; }

    public Guid CreatedBy { get; set; }
    public UserReadModel Creator { get; set; }

    public List<ReviewReadModel> Reviews { get; set; }
}