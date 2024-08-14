using DotNetBoilerplate.Application.Catalogs.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Catalogs.Browse;

public sealed record BrowseCatalogsQuery(Guid? BookStoreId = null) : IQuery<IEnumerable<CatalogDto>>;
