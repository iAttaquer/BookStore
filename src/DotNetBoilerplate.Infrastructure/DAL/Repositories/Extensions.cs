﻿using DotNetBoilerplate.Core.BookStores;
using DotNetBoilerplate.Core.Books;
using DotNetBoilerplate.Core.Users;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetBoilerplate.Infrastructure.DAL.Repositories;

internal static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, PostgresUserRepository>();
        services.AddScoped<IBookStoreRepository, PostgresBookStoreRepository>();
        services.AddSingleton<IBookRepository, InMemoryBookRepository>();

        return services;
    }
}