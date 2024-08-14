using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Application.Exceptions;

internal sealed class UsersCartDoesntExistException()
    : CustomException("Users cart does not exist.");