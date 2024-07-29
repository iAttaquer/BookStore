using DotNetBoilerplate.Core.Reviews;
using DotNetBoilerplate.Core.Users;

namespace DotNetBoilerplate.Core.Reviews;
public interface IReviewRepository
{
    Task AddAsync(Review review);

    Task<bool> UserAlreadyGaveReviewToThisBook(Guid bookId, UserId createdBy);

    Task UpdateAsync(Review review);

    Task <IEnumerable<Review>> GetAll();
    
    Task<Review> GetByIdAsync(Guid Id);

    Task<IEnumerable<Review>> GetAllReviewsToTheBook(Guid bookId);

    Task DeleteAsync(Review review);
}
