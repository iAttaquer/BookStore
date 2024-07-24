namespace DotNetBoilerplate.Application.Books.DTO;

public sealed record BookDto(
    Guid Id,
    string Title,
    string Writer,
    string Genre,
    int Year,
    string Description
);