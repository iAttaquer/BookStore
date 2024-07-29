using DotNetBoilerplate.Core.Reviews;
using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DotNetBoilerplate.Infrastructure.DAL.Repositories;

internal sealed class PostgresReviewsRepository(DotNetBoilerplateWriteDbContext dbContext) : IReviewRepository
{
     public async Task AddAsync(Review review)
    {
        await dbContext.AddAsync(review);
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> UserAlreadyGaveReviewToThisBook(Guid bookId, UserId createdBy)
    {
        return await dbContext.Reviews
            .AnyAsync(x=>x.BookId == bookId && x.CreatedBy == createdBy);
    }

    public async Task UpdateAsync(Review review)
    {
        dbContext.Update(review);
        await dbContext.SaveChangesAsync();
    }


    public async Task <IEnumerable<Review>> GetAll()
    {
        return await dbContext.Reviews.ToListAsync();
    }
    
    public async Task<Review> GetByIdAsync(Guid id)
    {
        return await dbContext.Reviews.FirstOrDefaultAsync(x=>x.Id==id);
    }

    public async Task<IEnumerable<Review>> GetAllReviewsToTheBook(Guid bookId)
    {
        return await dbContext.Reviews
            .Where(x=>x.BookId==bookId)
            .ToListAsync();
    }

    public async Task DeleteAsync(Review review)
    {
        dbContext.Reviews.Remove(review);
        await dbContext.SaveChangesAsync();
    }
}