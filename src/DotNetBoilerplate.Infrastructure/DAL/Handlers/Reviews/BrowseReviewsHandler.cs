using DotNetBoilerplate.Application.Reviews.DTO;
using DotNetBoilerplate.Core.Reviews;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace DotNetBoilerplate.Application.Reviews.Browse;

internal sealed class BrowseReviewsHandler(
    DotNetBoilerplateReadDbContext dbContext
) : IQueryHandler<BrowseReviewsQuery, IEnumerable<ReviewDto>>
{
    public async Task<IEnumerable<ReviewDto>> HandleAsync(BrowseReviewsQuery query)
    {
       return await dbContext.Reviews
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