namespace DotNetBoilerplate.Application.Catalogs.DTO;

public sealed record CatalogDto(
    Guid Id,
    string Name,
    string Genre,
    string Description
);