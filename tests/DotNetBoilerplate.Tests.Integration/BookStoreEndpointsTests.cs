using System.Net;
using System.Net.Http.Json;
using DotNetBoilerplate.Api.BookStores;
using DotNetBoilerplate.Api.Users;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using DotNetBoilerplate.Tests.Integration.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace DotNetBoilerplate.Tests.Integration;

[Collection(nameof(BookstoreEndpointsTestsCollection))]
public class BookStoreEndpointsTests(BookstoreEndpointsTestsFixture testsFixture) : IAsyncLifetime
{
    public async Task InitializeAsync()
    {
        await SignUpAndSignIn();
    }

    public async Task DisposeAsync()
    {
        await testsFixture.ResetDbChangesAsync();
        testsFixture.Client.DefaultRequestHeaders.Remove("Authorization");
    }

    private async Task SignUpAndSignIn()
    {
        var signUpRequest = new SignUpEndpoint.Request
        {
            Email = "email@t.pl",
            Password = "ttttttt",
            Username = "username"
        };

        var response = await testsFixture.Client.PostAsJsonAsync("users/sign-up", signUpRequest);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var signInRequest = new SignInEndpoint.Request
        {
            Email = "email@t.pl",
            Password = "ttttttt"
        };

        var signInResponse = await testsFixture.Client.PostAsJsonAsync("users/sign-in", signInRequest);

        var signInResult = await signInResponse.Content.ReadFromJsonAsync<SignInEndpoint.Response>();

        testsFixture.Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {signInResult!.Token}");
    }

    [Fact]
    public async Task GivenBookStoreDoesNotExist_CreateBookStore_ShouldSucceed()
    {
        //Arrange
        var request = new CreateBookStoreEndpoint.Request
        {
            Name = "BookStore",
            Description = "Description"
        };

        //Act
        var response = await testsFixture.Client.PostAsJsonAsync("book-stores", request);

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        using var serviceScope = testsFixture.ServiceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<DotNetBoilerplateReadDbContext>();

        var bookStore = await dbContext.BookStores.FirstOrDefaultAsync();

        bookStore.ShouldNotBeNull();
        bookStore!.Name.ShouldBe(request.Name);
    }
}