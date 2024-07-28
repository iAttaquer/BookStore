using DotNetBoilerplate.Application.Reviews.DTO;
using DotNetBoilerplate.Core.Reviews;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Reviews.Browse;

internal sealed class BrowseReviewsByBookIdHandler(
    IReviewRepository reviewRepository
) : IQueryHandler<BrowseReviewsByBookIdQuery, IEnumerable<ReviewDto>>
{
    public async Task<IEnumerable<ReviewDto>> HandleAsync(BrowseReviewsByBookIdQuery query)
    {
        var review = await reviewRepository.GetAllReviewsToTheBook(query.Id);

        return review
            .Select(x => new ReviewDto(x.Id, x.Name, x.Rating, x.Comment, x.BookId))
            .ToList();
    }
}