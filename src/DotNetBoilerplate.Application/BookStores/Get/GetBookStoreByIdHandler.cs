using DotNetBoilerplate.Application.BookStores.DTO;
using DotNetBoilerplate.Core.BookStores;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using DotNetBoilerplate.Application.Exceptions;
namespace DotNetBoilerplate.Application.BookStores.Get;

internal sealed class GetBookStoreByIdHandler(
    IBookStoreRepository bookStoreRepository
) : IQueryHandler<GetBookStoreByIdQuery, BookStoreDto>
{
    public async Task<BookStoreDto> HandleAsync(GetBookStoreByIdQuery query)
    {
        var bookStore = await bookStoreRepository.GetByIdAsync(query.Id);
        if (bookStore is null)
            return null;

        return new BookStoreDto(
                bookStore.Id,
                bookStore.Name,
                bookStore.Description
            );
    }
}