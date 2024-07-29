using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Reviews.Delete;

public sealed record DeleteReviewCommand(Guid reviewId) : ICommand<Guid>;
