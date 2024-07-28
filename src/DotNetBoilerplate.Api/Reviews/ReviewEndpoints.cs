using DotNetBoilerplate.Api.Reviews;

namespace DotNetBoilerplate.Api.Reviews;

internal static class ReviewEndpoints
{
    public const string BasePath = "reviews";
    public const string Tags = "Reviews";

    public static void MapReviewEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(BasePath)
            .WithTags(Tags);

        group
            .MapEndpoint<CreateReviewEndpoint>()
            .MapEndpoint<UpdateReviewEndpoint>()
            .MapEndpoint<DeleteReviewEndpoint>()
            .MapEndpoint<BrowseReviewEndpoint>()
            .MapEndpoint<BrowseReviewByBookIdEndpoint>()
            .MapEndpoint<GetReviewByIdEndpoint>();
    }
}
