using DotNetBoilerplate.Application.Exceptions;
using DotNetBoilerplate.Core.Reviews;
using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Contexts;

namespace DotNetBoilerplate.Application.Reviews.Update;

internal sealed class UpdateReviewHandler(
    IContext context,
    IReviewRepository reviewRepository
) : ICommandHandler<UpdateReviewCommand, Guid>
{
    public async Task<Guid> HandleAsync(UpdateReviewCommand command)
    {
        var review = await reviewRepository.GetByIdAsync(command.Id);
        if (review is null)
            throw new ReviewNotFoundException();
            
        bool userIsAdmin = context.Identity.Role == Role.Admin();

        review.Update(
            command.Name,
            command.Rating,
            command.Comment,
            userIsAdmin
        );

        await reviewRepository.UpdateAsync(review);
        return review.Id;
    }
}
