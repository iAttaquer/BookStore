using DotNetBoilerplate.Core.Users;

namespace DotNetBoilerplate.Core.BookStores;

public interface IBookStoreRepository
{
    Task AddAsync(BookStore bookStore);

    Task<BookStore> GetByIdAsync(Guid id);

    Task UpdateAsync(BookStore bookStore);

    Task<bool> UserAlreadyOwnsOrganizationAsync(UserId ownerId);

    Task<IEnumerable<BookStore>> GetAll();

    Task<BookStore> GetByOwnerIdAsync(UserId ownerId);
}