using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Application.Exceptions;

internal sealed class UserCanNotDeleteCatalogException()
    : CustomException("User does not have permission to delete this catalog.");
