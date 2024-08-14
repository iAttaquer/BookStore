using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.Carts.Exceptions;

internal sealed class IncorrectQuantityOfBooksException()
    : CustomException("Incorrect quantity of books.")
{
};