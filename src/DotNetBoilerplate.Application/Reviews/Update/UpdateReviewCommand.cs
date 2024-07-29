using DotNetBoilerplate.Core.Reviews;
using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Reviews.Update;

public sealed record UpdateReviewCommand(Guid Id, string Name, Rating Rating, 
    string Comment) : ICommand<Guid>;
