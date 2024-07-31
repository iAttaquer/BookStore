using DotNetBoilerplate.Core.Reviews;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using DotNetBoilerplate.Application.Exceptions;
using DotNetBoilerplate.Application.Reviews.DTO;
using DotNetBoilerplate.Application.Reviews.Get;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DotNetBoilerplate.Infrastructure.DAL.Handlers.Reviews;

internal sealed class GetReviewByIdHandler(
    DotNetBoilerplateReadDbContext dbContext
) : IQueryHandler<GetReviewByIdQuery, ReviewDto>
{
    public async Task<ReviewDto> HandleAsync(GetReviewByIdQuery query)
    {
        return await dbContext.Reviews
            .AsNoTracking()
            .Where(x=>x.Id==query.Id)
            .Select(x=>new ReviewDto(
                x.Id,
                x.Name,
                x.Rating,
                x.Comment,
                x.BookId
            ))
            .FirstOrDefaultAsync();
    }
}