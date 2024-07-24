using DotNetBoilerplate.Application.Books.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Books.Browse;

public sealed record BrowseBooksByBookStoreIdQuery(Guid Id) : IQuery<IEnumerable<BookDto>>;