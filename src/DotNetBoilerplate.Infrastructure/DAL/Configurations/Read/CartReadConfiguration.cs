using DotNetBoilerplate.Infrastructure.DAL.Configurations.Read.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Read;

internal sealed class CartReadConfiguration : IEntityTypeConfiguration<CartReadModel>
{
    public void Configure(EntityTypeBuilder<CartReadModel> builder)
    {
        builder.ToTable("Carts");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.OwnerModel)
            .WithOne(x => x.Cart)
            .HasForeignKey<CartReadModel>(x => x.Owner);

        builder.HasMany(x=>x.Items)
            .WithOne(x=>x.Cart)
            .HasForeignKey(x=>x.CartId);
    }
}