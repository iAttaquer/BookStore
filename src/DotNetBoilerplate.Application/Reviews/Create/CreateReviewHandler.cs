using DotNetBoilerplate.Core.Reviews;
using DotNetBoilerplate.Core.Books;
using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Application.Exceptions;

namespace DotNetBoilerplate.Application.Reviews.Create;

internal sealed class CreateReviewHandler(
    IContext context,
    IReviewRepository reviewRepository,
    IBookRepository bookRepository,
    IUserRepository userRepository
    ) : ICommandHandler<CreateReviewCommand, Guid>
{
    public async Task<Guid> HandleAsync(CreateReviewCommand command)
    {
        bool userAlreadyGaveReview = await reviewRepository.UserAlreadyGaveReviewToThisBook(command.BookId, context.Identity.Id);

        var book = await bookRepository.GetByIdAsync(command.BookId);
        if(book is null)
            throw new BookNotFoundException();

        var user = await userRepository.FindByIdAsync(context.Identity.Id);
        if(user is null)
            throw new UserNotFoundException(context.Identity.Id);
        
        var review = Review.Create(
            user.Username,
            command.Rating,
            command.Comment,
            command.BookId,
            context.Identity.Id,
            userAlreadyGaveReview
        );

        await reviewRepository.AddAsync(review);
        return review.Id;
    }
}
