using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.Books.Exceptions;

internal sealed class UserCanNotAddBookException()
    : CustomException("User can not add more than 100 Books.")
{
};