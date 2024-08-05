using Xunit;

namespace DotNetBoilerplate.Tests.Integration.Setup;

[CollectionDefinition(nameof(BookstoreEndpointsTestsCollection))]
public class BookstoreEndpointsTestsCollection : ICollectionFixture<BookstoreEndpointsTestsFixture>
{
}