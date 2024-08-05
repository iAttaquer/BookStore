using Xunit;

namespace DotNetBoilerplate.Tests.Integration.Setup;

[CollectionDefinition(nameof(ReviewEndpointsTestsCollection))]
public class ReviewEndpointsTestsCollection : ICollectionFixture<ReviewEndpointsTestsFixture>
{
}