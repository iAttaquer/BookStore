using DotNetBoilerplate.Application.Books.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Books.Browse;

public sealed record BrowseBooksQuery : IQuery<IEnumerable<BookDto>>;