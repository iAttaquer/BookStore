using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Application.Exceptions;

internal sealed class UserCanNotDeleteBookException()
    : CustomException("User does not have permission to delete this book.");
