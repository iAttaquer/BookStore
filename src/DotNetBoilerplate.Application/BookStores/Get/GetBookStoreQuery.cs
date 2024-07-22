using DotNetBoilerplate.Application.BookStores.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.BookStores.Get;

public sealed record GetBookStoreByIdQuery(Guid Id) : IQuery<BookStoreDto>;