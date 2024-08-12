using DotNetBoilerplate.Application.Books.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Catalogs.BrowseBooks;

public sealed record BrowseBooksInCatalogQuery(Guid CatalogId) : IQuery<IEnumerable<BookDto>>;