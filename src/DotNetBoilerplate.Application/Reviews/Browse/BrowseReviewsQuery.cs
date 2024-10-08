using DotNetBoilerplate.Application.Reviews.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Reviews.Browse;

public sealed record BrowseReviewsQuery(Guid? BookId = null) : IQuery<IEnumerable<ReviewDto>>;