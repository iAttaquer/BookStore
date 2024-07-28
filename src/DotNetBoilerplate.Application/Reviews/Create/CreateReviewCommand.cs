using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Reviews.Create;

public sealed record CreateReviewCommand(
    int Rating,
    string Comment,
    Guid BookId
 ) : ICommand<Guid>;
