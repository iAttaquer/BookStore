using DotNetBoilerplate.Core.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Write
{
    public class CartItemsWriteConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            // Primary Key
            builder.HasKey(ci => ci.Id);

            // Properties
            builder.Property(ci => ci.Id)
                .IsRequired();

            builder.Property(ci => ci.BookId)
                .IsRequired();

            builder.Property(ci => ci.Quantity)
                .HasConversion(ci=>ci.Value, ci=>new Quantity(ci))
                .IsRequired();

            // Relationships
            builder.HasOne<Cart>()
                .WithMany(c => c.Items)
                .HasForeignKey("CartId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
