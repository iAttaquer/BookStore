using DotNetBoilerplate.Core.Books;
using DotNetBoilerplate.Core.BookStores;
using DotNetBoilerplate.Core.Catalogs;
using DotNetBoilerplate.Core.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Write;

internal sealed class CatalogWriteConfiguration : IEntityTypeConfiguration<Catalog>
{
    public void Configure(EntityTypeBuilder<Catalog> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(x => x.Genre)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        // builder.Property(x => x.Books)
        //     .HasConversion<Book>()
        //     .IsRequired();

        builder.Property(x => x.BookStoreId)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .HasConversion(x => x.Value, x => new UserId(x))
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
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

        builder
            .HasMany<Book>()
            .WithMany()
            .UsingEntity(j => j.ToTable("CatalogBooks"));
    }
}