using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.Catalogs.Exceptions;

internal sealed class BookAlreadyAddedToCatalogException()
    : CustomException("Book is already added to a catalog.")
{
};
