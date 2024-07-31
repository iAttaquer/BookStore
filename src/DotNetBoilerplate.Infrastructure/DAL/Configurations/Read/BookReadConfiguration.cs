using DotNetBoilerplate.Infrastructure.DAL.Configurations.Read.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Read;

internal sealed class BookReadConfiguration : IEntityTypeConfiguration<BookReadModel>
{
    public void Configure(EntityTypeBuilder<BookReadModel> builder)
    {
        builder.ToTable("Books");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Creator)
            .WithMany(x => x.CreatedBooks)
            .HasForeignKey(x => x.CreatedBy);
    }
}