using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.Carts.Exceptions;

internal sealed class CartAlreadyExistsException()
    : CustomException("Users cart already exists.")
{
};