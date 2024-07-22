using DotNetBoilerplate.Application.BookStores.DTO;
using DotNetBoilerplate.Core.BookStores;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.BookStores.Browse;

internal sealed class BrowseBookStoresHandler(
    IBookStoreRepository bookStoreRepository
) : IQueryHandler<BrowseBookStoresQuery, IEnumerable<BookStoreDto>>
{
    public async Task<IEnumerable<BookStoreDto>> HandleAsync(BrowseBookStoresQuery query)
    {
        var bookStores = await bookStoreRepository.GetAll();

        return bookStores
            .Select(x => new BookStoreDto(x.Id, x.Name, x.Description))
            .ToList();
    }
}