﻿namespace DotNetBoilerplate.Api.Books;

internal static class BooksEndpoints
{
    public const string BasePath = "books";
    public const string Tags = "Books";

    public static void MapBookEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(BasePath)
            .WithTags(Tags);

        group
            .MapEndpoint<CreateBookEndpoint>()
            .MapEndpoint<UpdateBookEndpoint>()
            .MapEndpoint<DeleteBookEndpoint>()
            .MapEndpoint<GetBookByIdEndpoint>()
            .MapEndpoint<BrowseBooksEndpoint>();
    }
}
