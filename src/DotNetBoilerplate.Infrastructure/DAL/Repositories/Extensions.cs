using DotNetBoilerplate.Core.BookStores;
using DotNetBoilerplate.Core.Books;
using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Core.Reviews;
using Microsoft.Extensions.DependencyInjection;
using DotNetBoilerplate.Core.Catalogs;

namespace DotNetBoilerplate.Infrastructure.DAL.Repositories;

internal static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, PostgresUserRepository>();
        services.AddScoped<IBookStoreRepository, PostgresBookStoreRepository>();
<<<<<<< HEAD
        services.AddScoped<IReviewRepository, PostgresReviewsRepository>();
        services.AddScoped<IBookRepository, PostgresBookRepository>();
=======
        services.AddSingleton<IBookRepository, InMemoryBookRepository>();
>>>>>>> 14b5db90cce474f25e32d9df42e17307869a3001
        services.AddSingleton<ICatalogRepository, InMemoryCatalogRepository>();

        return services;
    }
}