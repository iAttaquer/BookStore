using DotNetBoilerplate.Core.Reviews;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Application.Exceptions;

namespace DotNetBoilerplate.Application.Reviews.Delete;

internal sealed class DeleteReviewHandler(
    IContext context,
    IReviewRepository reviewRepository
    ) : ICommandHandler<DeleteReviewCommand>
{
    public async Task HandleAsync(DeleteReviewCommand command)
    {
        var review = await reviewRepository.GetByIdAsync(command.reviewId);

        if (review is null)
            throw new ReviewNotFoundException();
        if (review.CreatedBy != context.Identity.Id)
            throw new UserCanNotDeleteReviewException();

        await reviewRepository.DeleteAsync(review);
    }
}
