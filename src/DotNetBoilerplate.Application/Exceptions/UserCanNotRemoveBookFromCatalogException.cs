using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Application.Exceptions;

internal sealed class UserCanNotRemoveBookFromCatalogException()
    : CustomException("User can not remove book from that catalog.");