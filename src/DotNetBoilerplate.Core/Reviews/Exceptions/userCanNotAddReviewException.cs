using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.Reviews.Exceptions;

internal sealed class UserCanNotAddReviewException()
    : CustomException("User can not add more than 1 review to the same book")
{
};