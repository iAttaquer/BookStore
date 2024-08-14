namespace DotNetBoilerplate.Api.Carts;

internal static class CartsEndpoints
{
    public const string BasePath = "cart";
    public const string Tags = "Carts";

    public static void MapCartsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(BasePath)
            .WithTags(Tags);

        group
            .MapEndpoint<CreateCartEndpoint>()
            .MapEndpoint<UpdateCartEndpoint>()
            .MapEndpoint<GetCartByIdEndpoint>()
            .MapEndpoint<BrowseCartsEndpoint>()
            .MapEndpoint<DeleteCartEndpoint>();
            
    }
}
