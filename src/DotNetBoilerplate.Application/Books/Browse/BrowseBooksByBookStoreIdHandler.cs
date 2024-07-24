using DotNetBoilerplate.Application.Books.DTO;
using DotNetBoilerplate.Core.Books;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Books.Browse;

internal sealed class BrowseBooksByBookStoreIdHandler(
    IBookRepository bookRepository
) : IQueryHandler<BrowseBooksByBookStoreIdQuery, IEnumerable<BookDto>>
{
    public async Task<IEnumerable<BookDto>> HandleAsync(BrowseBooksByBookStoreIdQuery query)
    {
        var books = await bookRepository.GetAllInBookStore(query.Id);

        return books
            .Select(x => new BookDto(x.Id, x.Title, x.Writer, x.Genre, x.Year, x.Description))
            .ToList();
    }
}