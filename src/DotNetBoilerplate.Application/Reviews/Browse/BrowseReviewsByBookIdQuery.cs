using DotNetBoilerplate.Application.Reviews.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Reviews.Browse;

public sealed record BrowseReviewsByBookIdQuery(Guid Id) : IQuery<IEnumerable<ReviewDto>>;