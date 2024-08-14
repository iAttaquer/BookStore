using DotNetBoilerplate.Application.Catalogs.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Catalogs.Get;

public sealed record GetCatalogByIdQuery(Guid Id) : IQuery<CatalogDto>;