using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.BookStores.Exceptions;

internal sealed class UserCanNotUpdateOrganizationException()
    : CustomException("User can not update organization.")
{
};
