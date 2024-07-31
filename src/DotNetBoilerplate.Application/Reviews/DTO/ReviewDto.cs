using DotNetBoilerplate.Core.Reviews;

namespace DotNetBoilerplate.Application.Reviews.DTO;

public sealed record ReviewDto(
    Guid Id,
    string Name,
    int Rating,
    string Comment,
    Guid bookId
);