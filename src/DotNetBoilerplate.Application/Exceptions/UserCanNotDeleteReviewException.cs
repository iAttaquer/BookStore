using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Application.Exceptions;

internal sealed class UserCanNotDeleteReviewException()
    : CustomException("User can not delete review.");