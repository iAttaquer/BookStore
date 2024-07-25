using System.Net;
using System.Net.Http.Json;
using DotNetBoilerplate.Api.Users;
using Shouldly;
using Xunit;

namespace DotNetBoilerplate.Tests.Integration;

[Collection(nameof(UserEndpointsTestsCollection))]
public class UserEndpointsTests(UserEndpointsTestsFixture testsFixture) : IAsyncLifetime
{
    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await testsFixture.ResetDbChangesAsync();
    }

    [Fact]
    public async Task GivenEmailIsUnique_SignUp_ShouldSucceed()
    {
        //Arrange
        var request = new SignUpEndpoint.Request
        {
            Email = "email@t.pl",
            Password = "ttttttt",
            Username = "username"
        };

        //Act
        var response = await testsFixture.Client.PostAsJsonAsync("users/sign-up", request);

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        //SignIn
        var signInRequest = new SignInEndpoint.Request
        {
            Email = "email@t.pl",
            Password = "ttttttt"
        };

        var signInResponse = await testsFixture.Client.PostAsJsonAsync("users/sign-in", signInRequest);

        var signInResult = await signInResponse.Content.ReadFromJsonAsync<SignInEndpoint.Response>();

        signInResult.ShouldNotBeNull();
    }

    [Fact]
    public async Task GivenEmailIsNotUnique_SignUp_ShouldFail()
    {
        //Arrange
        var request = new SignUpEndpoint.Request
        {
            Email = "email@t.pl",
            Password = "ttttttt",
            Username = "username"
        };

        await testsFixture.Client.PostAsJsonAsync("users/sign-up", request);

        //Act
        var response = await testsFixture.Client.PostAsJsonAsync("users/sign-up", request);

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
}