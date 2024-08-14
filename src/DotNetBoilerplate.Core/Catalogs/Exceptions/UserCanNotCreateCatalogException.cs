using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.Catalogs.Exceptions;

internal sealed class UserCanNotCreateCatalogException()
    : CustomException("User can not create catalog.")
{
};
