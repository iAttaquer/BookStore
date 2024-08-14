using DotNetBoilerplate.Application.Books.DTO;
using DotNetBoilerplate.Core.Catalogs;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Catalogs.BrowseBooks;

internal sealed class BrowseBooksInCatalogHandler(
    ICatalogRepository catalogRepository
) : IQueryHandler<BrowseBooksInCatalogQuery, IEnumerable<BookDto>>
{
    public async Task<IEnumerable<BookDto>> HandleAsync(BrowseBooksInCatalogQuery query)
    {
        var books = await catalogRepository.GetBooksInCatalogAsync(query.CatalogId);

        return books.Select(x => new BookDto(x.Id, x.Title, x.Writer, x.Genre, x.Year, x.Description));
    }
}