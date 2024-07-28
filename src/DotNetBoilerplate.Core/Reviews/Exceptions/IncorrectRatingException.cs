using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.Reviews.Exceptions;

internal sealed class IncorrectRatingException()
    : CustomException("Rating can only be in scale 0-10.")
{
};