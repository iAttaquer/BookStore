using DotNetBoilerplate.Core.Reviews.Exceptions;
using DotNetBoilerplate.Core.Users;

namespace DotNetBoilerplate.Core.Reviews;

public class Rating
{
    public Rating(int value){
        if(value < 0 || value > 10) throw new IncorrectRatingException();
        Value = value;
    }
    
    public int Value { get; set; }
}

public class Review
{
    private Review(){}

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Rating Rating { get; private set; }
    public string Comment { get; private set; }
    public Guid BookId { get; private set; }
    public UserId CreatedBy { get; private set; }

    public static Review Create(
        string name,
        Rating rating,
        string comment,
        Guid bookId,
        Guid createdBy,
        bool userAlreadyGaveReviewToThisBook
    ){

        if(userAlreadyGaveReviewToThisBook)
            throw new UserCanNotAddReviewException();

        return new Review
        {
            Id = Guid.NewGuid(),
            Name = name,
            Rating = rating,
            Comment = comment,
            BookId = bookId,
            CreatedBy = createdBy
        };
    }

    public void Update(
        string name,
        Rating rating,
        string comment,
        bool userIsAdmin
    ){
        if(!userIsAdmin)
            throw new OnlyAdministratorCanUpdateReviewsException();

        Name = name;
        Rating = rating;
        Comment=comment;
    }
}
