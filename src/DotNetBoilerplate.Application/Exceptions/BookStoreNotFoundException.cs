using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Application.Exceptions;

internal sealed class BookStoreNotFoundException()
    : CustomException("BookStore not found");
