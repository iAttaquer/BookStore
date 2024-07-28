using DotNetBoilerplate.Application.Reviews.DTO;
using DotNetBoilerplate.Core.Reviews;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Reviews.Browse;

internal sealed class BrowseReviewsHandler(
    IReviewRepository reviewRepository
) : IQueryHandler<BrowseReviewsQuery, IEnumerable<ReviewDto>>
{
    public async Task<IEnumerable<ReviewDto>> HandleAsync(BrowseReviewsQuery query)
    {
        var review = await reviewRepository.GetAll();

        return review
            .Select(x => new ReviewDto(x.Id, x.Name, x.Rating, x.Comment, x.BookId))
            .ToList();
    }
}