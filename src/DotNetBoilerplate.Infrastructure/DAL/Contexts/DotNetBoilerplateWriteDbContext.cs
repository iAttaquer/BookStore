using DotNetBoilerplate.Core.Books;
using DotNetBoilerplate.Core.BookStores;
using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Core.Reviews;
using DotNetBoilerplate.Core.Catalogs;
using DotNetBoilerplate.Infrastructure.DAL.Configurations.Write;
using DotNetBoilerplate.Shared.Abstractions.Outbox;
using Microsoft.EntityFrameworkCore;

namespace DotNetBoilerplate.Infrastructure.DAL.Contexts;

internal sealed class DotNetBoilerplateWriteDbContext(DbContextOptions<DotNetBoilerplateWriteDbContext> options)
    : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public DbSet<BookStore> BookStores { get; set; }

    public DbSet<Book> Books { get; set; }

    public DbSet<Review> Reviews { get; set; }

    public DbSet<Catalog> Catalogs { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dotNetBoilerplate");

        modelBuilder.ApplyConfiguration(new UserWriteConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageWriteConfiguration());
        modelBuilder.ApplyConfiguration(new BookStoreWriteConfiguration());
        modelBuilder.ApplyConfiguration(new BookWriteConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewsWriteConfiguration());
        modelBuilder.ApplyConfiguration(new CatalogWriteConfiguration());
    }
}