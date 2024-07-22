using DotNetBoilerplate.Api.BookStores;

namespace DotNetBoilerplate.Api.BookStrores;

internal static class BookStoresEndpoints
{
    public const string BasePath = "book-stores";
    public const string Tags = "Book Stores";

    public static void MapBookStoresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(BasePath)
            .WithTags(Tags);

        group.MapEndpoint<CreateBookStoreEndpoint>()
            .MapEndpoint<UpdateBookStoreEndpoint>()
            .MapEndpoint<BrowseBookStoreEndpoint>()
            .MapEndpoint<GetBookStoreByIdEndpoint>();
    }
}