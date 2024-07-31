namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Read.Model;

internal sealed class ReviewReadModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }

    public Guid BookId { get; set; }
    public BookReadModel Book { get; set; }

    public Guid CreatedBy { get; set; }
    public UserReadModel Creator { get; set; }
}