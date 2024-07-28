using DotNetBoilerplate.Core.Reviews;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using DotNetBoilerplate.Application.Exceptions;
using DotNetBoilerplate.Application.Reviews.DTO;
namespace DotNetBoilerplate.Application.Reviews.Get;

internal sealed class GetReviewByIdHandler(
    IReviewRepository reviewRepository
) : IQueryHandler<GetReviewByIdQuery, ReviewDto>
{
    public async Task<ReviewDto> HandleAsync(GetReviewByIdQuery query)
    {
        var review = await reviewRepository.GetByIdAsync(query.Id);
        if (review is null)
            return null;

        return new ReviewDto(
            review.Id,
            review.Name,
            review.Rating,
            review.Comment,
            review.BookId
        );
    }
}