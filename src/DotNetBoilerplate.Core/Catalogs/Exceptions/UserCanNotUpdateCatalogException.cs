using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.Catalogs.Exceptions;

internal sealed class UserCanNotUpdateCatalogException()
    : CustomException("User can not update catalog.")
{
};
