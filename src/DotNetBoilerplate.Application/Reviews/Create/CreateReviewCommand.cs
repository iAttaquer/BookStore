using DotNetBoilerplate.Core.Reviews;
using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Reviews.Create;

public sealed record CreateReviewCommand(
    Rating Rating,
    string Comment,
    Guid? BookId = null
 ) : ICommand<Guid>;
