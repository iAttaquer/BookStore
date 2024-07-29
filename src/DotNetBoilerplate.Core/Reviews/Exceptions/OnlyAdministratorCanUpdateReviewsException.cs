using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.Reviews.Exceptions;

internal sealed class OnlyAdministratorCanUpdateReviewsException()
    : CustomException("Only administrator can update existing review.")
{
};