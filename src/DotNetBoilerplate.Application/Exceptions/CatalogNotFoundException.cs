using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Application.Exceptions;

internal sealed class CatalogNotFoundException()
    : CustomException("Catalog not found");
