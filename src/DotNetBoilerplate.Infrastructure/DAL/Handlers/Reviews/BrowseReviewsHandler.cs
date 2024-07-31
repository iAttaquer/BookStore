using DotNetBoilerplate.Application.Reviews.DTO;
using DotNetBoilerplate.Core.Reviews;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;
using DotNetBoilerplate.Application.Reviews.Browse;

namespace DotNetBoilerplate.Infrastructure.DAL.Handlers.Reviews;

internal sealed class BrowseReviewsHandler(
    DotNetBoilerplateReadDbContext dbContext
) : IQueryHandler<BrowseReviewsQuery, IEnumerable<ReviewDto>>
{
    public async Task<IEnumerable<ReviewDto>> HandleAsync(BrowseReviewsQuery query)
    {
        var reviewsQuery = dbContext.Reviews.AsNoTracking();
        if(query.BookId.HasValue) reviewsQuery = reviewsQuery.Where(x=>x.BookId==query.BookId.Value);
       
       return await reviewsQuery
        .AsNoTracking()
        .Select(x=>new ReviewDto(
            x.Id,
            x.Name,
            x.Rating,
            x.Comment,
            x.BookId
        ))
        .ToListAsync();
    }
}