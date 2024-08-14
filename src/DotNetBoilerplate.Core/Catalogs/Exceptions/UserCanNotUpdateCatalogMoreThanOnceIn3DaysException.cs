using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.Catalogs.Exceptions;

internal sealed class UserCanNotUpdateCatalogMoreThanOnceIn3DaysException()
    : CustomException("User can not update catalog more than once in 3 days.")
{
};
