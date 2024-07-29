using DotNetBoilerplate.Core.Books;
using DotNetBoilerplate.Core.Reviews;
using DotNetBoilerplate.Core.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Write;

internal sealed class ReviewsWriteConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Rating)
            .HasConversion(x => x.Value, x => new Rating(x));

        builder.Property(x => x.Rating)
            .IsRequired();

        builder.Property(x => x.Comment)
            .HasMaxLength(300);

        builder
            .HasOne<Book>()
            .WithMany()
            .HasForeignKey(x => x.BookId)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .HasConversion(x => x.Value, x => new UserId(x));

        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.CreatedBy)
            .IsRequired();

        builder.HasIndex(r => new { r.BookId, r.CreatedBy })
                .IsUnique();
    }
}