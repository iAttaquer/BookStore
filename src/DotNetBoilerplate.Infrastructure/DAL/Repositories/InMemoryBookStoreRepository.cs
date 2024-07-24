using DotNetBoilerplate.Core.BookStores;
using DotNetBoilerplate.Core.Users;

namespace DotNetBoilerplate.Infrastructure.DAL.Repositories;

internal sealed class InMemoryBookStoreRepository : IBookStoreRepository
{
    private readonly List<BookStore> _bookStores = [];

    public Task AddAsync(BookStore bookStore)
    {
        _bookStores.Add(bookStore);
        return Task.CompletedTask;
    }

    public Task<BookStore> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_bookStores.FirstOrDefault(x => x.Id == id));
    }

    public Task UpdateAsync(BookStore bookStore)
    {
        var existingBookStoreIndex = _bookStores.FindIndex(x => x.Id == bookStore.Id);
        _bookStores[existingBookStoreIndex] = bookStore;

        return Task.CompletedTask;
    }

    public Task<IEnumerable<BookStore>> GetAll()
    {
        return Task.FromResult(_bookStores.AsEnumerable());
    }

    public Task<bool> UserAlreadyOwnsOrganizationAsync(UserId ownerId)
    {
        return Task.FromResult(_bookStores.Any(x => x.OwnerId == ownerId));
    }

    public Task<BookStore> GetByOwnerIdAsync(UserId ownerId)
    {
        return Task.FromResult(_bookStores.FirstOrDefault(x => x.OwnerId == ownerId));
    }
}