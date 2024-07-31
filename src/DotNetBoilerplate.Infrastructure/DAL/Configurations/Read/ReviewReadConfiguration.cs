using DotNetBoilerplate.Infrastructure.DAL.Configurations.Read.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Read;

internal sealed class ReviewReadConfiguration : IEntityTypeConfiguration<ReviewReadModel>
{
    public void Configure(EntityTypeBuilder<ReviewReadModel> builder)
    {
        builder.ToTable("Reviews");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Book)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.BookId);

        builder.HasOne(x => x.Creator)
            .WithMany(x => x.CreatedReviews)
            .HasForeignKey(x => x.BookId);
    }
}