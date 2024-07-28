using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Reviews.Update;

public sealed record UpdateReviewCommand(Guid Id, string Name, int Rating, 
    string Comment) : ICommand<Guid>;
