using DotNetBoilerplate.Infrastructure.DAL.Configurations.Read.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Read;

internal sealed class CartItemReadConfiguration : IEntityTypeConfiguration<CartItemReadModel>
{
    public void Configure(EntityTypeBuilder<CartItemReadModel> builder)
    {
        builder.ToTable("CartItems");

        builder.HasKey(x => x.Id);

        builder.HasOne(x=>x.Cart)
            .WithMany(x=>x.Items)
            .HasForeignKey(x=>x.CartId);
    }
}