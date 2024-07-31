using DotNetBoilerplate.Infrastructure.DAL.Configurations.Read.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Read;

internal sealed class BookStoreReadConfiguration : IEntityTypeConfiguration<BookStoreReadModel>
{
    public void Configure(EntityTypeBuilder<BookStoreReadModel> builder)
    {
        builder.ToTable("BookStores");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Owner)
            .WithOne(x => x.BookStore)
            .HasForeignKey<BookStoreReadModel>(x => x.OwnerId);
    }
}