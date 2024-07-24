using DotNetBoilerplate.Application.Books.DTO;
using DotNetBoilerplate.Core.Books;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using DotNetBoilerplate.Application.Exceptions;
namespace DotNetBoilerplate.Application.Books.Get;

internal sealed class GetBookStoreByIdHandler(
    IBookRepository bookRepository
) : IQueryHandler<GetBookByIdQuery, BookDto>
{
    public async Task<BookDto> HandleAsync(GetBookByIdQuery query)
    {
        var book = await bookRepository.GetByIdAsync(query.Id);
        if (book is null)
            return null;

        return new BookDto(
                book.Id,
                book.Title,
                book.Writer,
                book.Genre,
                book.Year,
                book.Description
            );
    }
}