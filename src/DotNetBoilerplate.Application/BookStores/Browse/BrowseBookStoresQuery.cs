using DotNetBoilerplate.Application.BookStores.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.BookStores.Browse;

public sealed record BrowseBookStoresQuery : IQuery<IEnumerable<BookStoreDto>>;