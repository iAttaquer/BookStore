using DotNetBoilerplate.Core.Books;
using DotNetBoilerplate.Core.BookStores;
using DotNetBoilerplate.Core.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Write;

internal sealed class BookWriteConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(x => x.Writer)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(x => x.Genre)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.Year)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.BookStoreId)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .HasConversion(x => x.Value, x => new UserId(x))
            .IsRequired();

        builder
            .HasOne<BookStore>()
            .WithMany()
            .HasForeignKey(x => x.BookStoreId)
            .IsRequired();

        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.CreatedBy)
            .IsRequired();


    }
}