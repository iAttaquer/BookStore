using DotNetBoilerplate.Api.Catalogs;

namespace DotNetBoilerplate.Api.Catalogs;

internal static class CatalogsEndpoints
{
    public const string BasePath = "catalogs";
    public const string Tags = "Catalogs";

    public static void MapCatalogsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(BasePath)
            .WithTags(Tags);

        group
            .MapEndpoint<CreateCatalogEndpoint>()
            .MapEndpoint<UpdateCatalogEndpoint>();
    }
}

