using DotNetBoilerplate.Core.Reviews;
using DotNetBoilerplate.Core.Users;

namespace DotNetBoilerplate.Infrastructure.DAL.Repositories;

internal sealed class InMemoryReviewRepository : IReviewRepository
{
    private readonly List<Review> _reviews = [];

    public Task AddAsync(Review review)
    {
        _reviews.Add(review);
        return Task.CompletedTask;
    }

    public Task<bool> userAlreadyGaveReviewToThisBook(Guid bookId, UserId createdBy)
    {
        return Task.FromResult(_reviews.Any(x=>x.BookId == bookId && x.CreatedBy == createdBy));
    }

    public Task UpdateAsync(Review review)
    {
        var existingReview = _reviews.FindIndex(x => x.Id == review.Id);
        _reviews[existingReview] = review;
        
        return Task.CompletedTask;
    }


    public Task <IEnumerable<Review>> GetAll()
    {
        return Task.FromResult(_reviews.AsEnumerable());
    }
    
    public Task<Review> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_reviews.FirstOrDefault(x => x.Id == id));
    }

    public Task<IEnumerable<Review>> GetAllReviewsToTheBook(Guid bookId)
    {
        return Task.FromResult(_reviews.FindAll(x => x.BookId == bookId)
            .AsEnumerable());
    }

    public Task DeleteAsync(Review review)
    {
        _reviews.Remove(review);
        return Task.CompletedTask;
    }
}