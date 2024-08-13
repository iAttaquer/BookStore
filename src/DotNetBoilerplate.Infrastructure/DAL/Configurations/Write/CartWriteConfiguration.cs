using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Core.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Write
{
    public class CartsWriteConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Owner)
                .HasConversion(x => x.Value, x => new UserId(x))
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasMany(c => c.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.Owner);
        }
    }
}
