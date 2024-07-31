using DotNetBoilerplate.Application.Books.DTO;
using DotNetBoilerplate.Core.Books;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Books.Browse;

internal sealed class BrowseBooksHandler(
    IBookRepository bookRepository
) : IQueryHandler<BrowseBooksQuery, IEnumerable<BookDto>>
{
    public async Task<IEnumerable<BookDto>> HandleAsync(BrowseBooksQuery query)
    {
        var books = await bookRepository.GetAll();

        return books
            .Select(x => new BookDto(x.Id, x.Title, x.Writer, x.Genre, x.Year, x.Description, []))
            .ToList();
    }
}