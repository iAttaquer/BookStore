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

    [Fact]
    public async Task UpdateBookStore_ShouldSucceed()
    {
        //Arrange
        var createRequest = new CreateBookStoreEndpoint.Request
        {
            Name = "BookStore",
            Description = "Description"
        };

        var createResponse = await testsFixture.Client.PostAsJsonAsync("book-stores", createRequest);
        createResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        using var serviceScope = testsFixture.ServiceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<DotNetBoilerplateReadDbContext>();

        var bookStore = await dbContext.BookStores.FirstOrDefaultAsync();
        bookStore.ShouldNotBeNull();
        bookStore!.Name.ShouldBe(createRequest.Name);


        var updateRequest = new UpdateBookStoreEndpoint.Request
        {
            Name = "Updated book store",
            Description = "Updated description"
        };

        //Act
        var updateResponse = await testsFixture.Client.PutAsJsonAsync($"book-stores/{bookStore.Id}", updateRequest);

         //Assert
        updateResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        using var updateServiceScope = testsFixture.ServiceProvider.CreateScope();
        var updateDbContext = updateServiceScope.ServiceProvider.GetRequiredService<DotNetBoilerplateReadDbContext>();

        var updatedBookStore = await updateDbContext.BookStores.FirstOrDefaultAsync(x=>x.Id==bookStore.Id);

        updatedBookStore.ShouldNotBeNull();
        updatedBookStore!.Name.ShouldBe(updateRequest.Name);
    }

    [Fact]
    public async Task UpdateBookStore_WithWrongId_ShouldReturnBadRequest()
    {
        //Arrange
        var createRequest = new CreateBookStoreEndpoint.Request
        {
            Name = "BookStore",
            Description = "Description"
        };

        var createResponse = await testsFixture.Client.PostAsJsonAsync("book-stores", createRequest);
        createResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        using var serviceScope = testsFixture.ServiceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<DotNetBoilerplateReadDbContext>();

        var bookStore = await dbContext.BookStores.FirstOrDefaultAsync();
        bookStore.ShouldNotBeNull();
        bookStore!.Name.ShouldBe(createRequest.Name);

        var updateRequest = new UpdateBookStoreEndpoint.Request
        {
            Name = "Updated book store",
            Description = "Updated description"
        };

        var randomId = Guid.NewGuid();
        //Act
        var updateResponse = await testsFixture.Client.PutAsJsonAsync($"book-stores/{randomId}", updateRequest);

         //Assert
        updateResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

    }

    [Fact]
    public async Task GetBookStore_ShouldSucceed()
    {
        //Arrange
        var request = new CreateBookStoreEndpoint.Request
        {
            Name = "BookStore",
            Description = "Description"
        };

        var response = await testsFixture.Client.PostAsJsonAsync("book-stores", request);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        using var serviceScope = testsFixture.ServiceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<DotNetBoilerplateReadDbContext>();

        var bookStore = await dbContext.BookStores.FirstOrDefaultAsync();

        bookStore.ShouldNotBeNull();
        bookStore!.Name.ShouldBe(request.Name);

        //Act
        await testsFixture.Client.GetAsync($"book-stores/{bookStore.Id}");

        //Assert
        using var getServiceScope = testsFixture.ServiceProvider.CreateScope();
        var getDbContext = getServiceScope.ServiceProvider.GetRequiredService<DotNetBoilerplateReadDbContext>();

        var getBookStore = await getDbContext.BookStores.FirstOrDefaultAsync(x=>x.Id==bookStore.Id);

        getBookStore.ShouldNotBeNull();
        getBookStore!.Name.ShouldBe(bookStore.Name);
    }
}