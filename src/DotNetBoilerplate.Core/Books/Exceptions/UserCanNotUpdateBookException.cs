using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.Books.Exceptions;

internal sealed class UserCanNotUpdateBookException()
    : CustomException("User can not update book.")
{
};