namespace DotNetBoilerplate.Application.BookStores.DTO;

public sealed record BookStoreDto(
    Guid Id,
    string Name,
    string Description,
    Guid OwnerId
);