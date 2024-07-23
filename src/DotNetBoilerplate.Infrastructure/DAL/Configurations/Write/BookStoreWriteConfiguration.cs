using DotNetBoilerplate.Core.BookStores;
using DotNetBoilerplate.Core.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Write;

internal sealed class BookStoreWriteConfiguration : IEntityTypeConfiguration<BookStore>
{
    public void Configure(EntityTypeBuilder<BookStore> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.OwnerId)
            .HasConversion(x => x.Value, x => new UserId(x));

        builder
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<BookStore>(x => x.OwnerId);
    }
}