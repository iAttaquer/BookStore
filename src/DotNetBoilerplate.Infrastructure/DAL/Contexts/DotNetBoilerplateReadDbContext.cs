using DotNetBoilerplate.Infrastructure.DAL.Configurations.Read;
using DotNetBoilerplate.Infrastructure.DAL.Configurations.Read.Model;
using Microsoft.EntityFrameworkCore;

namespace DotNetBoilerplate.Infrastructure.DAL.Contexts;

internal sealed class DotNetBoilerplateReadDbContext(DbContextOptions<DotNetBoilerplateReadDbContext> options)
    : DbContext(options)
{
    public DbSet<UserReadModel> Users { get; set; }
    public DbSet<BookReadModel> Books { get; set; }
    public DbSet<ReviewReadModel> Reviews { get; set; }
    public DbSet<CartReadModel> Carts {get; set; }
    public DbSet<CartItemReadModel> CartItem { get; set;}
    public DbSet<BookStoreReadModel> BookStores { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dotNetBoilerplate");
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserReadConfiguration());
        modelBuilder.ApplyConfiguration(new BookReadConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewReadConfiguration());
        modelBuilder.ApplyConfiguration(new BookStoreReadConfiguration());
        modelBuilder.ApplyConfiguration(new CartReadConfiguration());
        modelBuilder.ApplyConfiguration(new CartItemReadConfiguration());
    }
}