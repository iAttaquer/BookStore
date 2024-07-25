using Xunit;

namespace DotNetBoilerplate.Tests.Integration;

[CollectionDefinition(nameof(UserEndpointsTestsCollection))]
public class UserEndpointsTestsCollection : ICollectionFixture<UserEndpointsTestsFixture>
{
}