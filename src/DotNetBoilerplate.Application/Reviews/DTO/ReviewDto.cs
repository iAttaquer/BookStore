using DotNetBoilerplate.Core.Reviews;

namespace DotNetBoilerplate.Application.Reviews.DTO;

public sealed record ReviewDto(
    Guid Id,
    string Name,
    Rating Rating,
    string Comment,
    Guid bookId
);