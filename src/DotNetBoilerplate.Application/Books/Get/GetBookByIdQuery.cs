using DotNetBoilerplate.Application.Books.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Books.Get;

public sealed record GetBookByIdQuery(Guid Id) : IQuery<BookDto>;