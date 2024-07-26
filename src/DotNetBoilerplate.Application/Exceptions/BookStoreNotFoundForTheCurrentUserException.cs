using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Application.Exceptions;

internal sealed class BookStoreNotFoundForTheCurrentUserException()
    : CustomException("BookStore not found for the current user");
