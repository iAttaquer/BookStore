using Xunit;

namespace DotNetBoilerplate.Tests.Integration.Setup;

[CollectionDefinition(nameof(BookEndpointsTestsCollection))]
public class BookEndpointsTestsCollection : ICollectionFixture<BookEndpointsTestsFixture>
{
}