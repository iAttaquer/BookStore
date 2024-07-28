using DotNetBoilerplate.Application.Reviews.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Reviews.Get;

public sealed record GetReviewByIdQuery(Guid Id) : IQuery<ReviewDto>;