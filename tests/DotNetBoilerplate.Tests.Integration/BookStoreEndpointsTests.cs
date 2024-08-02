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

    private async Task SignUpAndSignIn(string mail = "email@t.pl",
        string pass = "ttttttt", string username = "username")
    {
        var signUpRequest = new SignUpEndpoint.Request
        {
            Email = mail,
            Password = pass,
            Username = username
        };

        var response = await testsFixture.Client.PostAsJsonAsync("users/sign-up", signUpRequest);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var signInRequest = new SignInEndpoint.Request
        {
            Email = mail,
            Password = pass
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
    public async Task GivenBookStoreExists_CreateBookStore_ShouldFail()
    {
        //Arrange
        var existingBookStore = new CreateBookStoreEndpoint.Request
        {
            Name = "BookStore",
            Description = "Description"
        };
        var request = new CreateBookStoreEndpoint.Request
        {
            Name = "BookStore2",
            Description = "Description2"
        };

        //Act
        var firstResponse = await testsFixture.Client.PostAsJsonAsync("book-stores", existingBookStore);
        var secondResponse = await testsFixture.Client.PostAsJsonAsync("book-stores", request);

        //Assert
        firstResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        secondResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        using var serviceScope = testsFixture.ServiceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<DotNetBoilerplateReadDbContext>();

        var bookStores = await dbContext.BookStores.ToListAsync();
        bookStores.Count.ShouldBe(1);

        var bookStore = await dbContext.BookStores.FirstOrDefaultAsync();
        bookStore.ShouldNotBeNull();
        bookStore!.Name.ShouldBe(existingBookStore.Name);
    }

    public async Task GivenBookStoreExists_UpdateBookStore_ShouldSucceed()
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

        var updatedBookStore = await dbContext.BookStores.FirstOrDefaultAsync(x=>x.Id==bookStore.Id);

        updatedBookStore.ShouldNotBeNull();
        updatedBookStore!.Name.ShouldBe(updateRequest.Name);
    }

    [Fact]
    public async Task GivenBookStoreDoesNotExists_UpdateBookStore_ShouldFail()
    {
        //Arrange
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
    public async Task GivenBookStoresExists_BrowseBookStores_ShouldReturnAll()
    {
        //Arrange
        var firstBookStore = new CreateBookStoreEndpoint.Request
        {
            Name = "BookStore",
            Description = "Description"
        };

        var secondBookStore = new CreateBookStoreEndpoint.Request
        {
            Name = "BookStore2",
            Description = "Description2"
        };

        //Act
        var firstResponse = await testsFixture.Client.PostAsJsonAsync("book-stores", firstBookStore);

        testsFixture.Client.DefaultRequestHeaders.Remove("Authorization");
        await SignUpAndSignIn("email2@t.pl", "ttttttt2", "username2");

        var secondResponse = await testsFixture.Client.PostAsJsonAsync("book-stores", secondBookStore);

        //Assert
        firstResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        secondResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        using var serviceScope = testsFixture.ServiceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<DotNetBoilerplateReadDbContext>();

        var bookStores = await dbContext.BookStores.ToListAsync();
        bookStores.Count.ShouldBe(2);
    }

    [Fact]
    public async Task GivenBookStoreExists_GetBookStoreById_ShouldSucceed()
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

        //Act
        await testsFixture.Client.GetAsync($"book-stores/{bookStore.Id}");

        //Assert

        bookStore.ShouldNotBeNull();
        bookStore!.Name.ShouldBe(request.Name);

        var getBookStore = await dbContext.BookStores.FirstOrDefaultAsync(x=>x.Id==bookStore.Id);

        getBookStore.ShouldNotBeNull();
        getBookStore!.Name.ShouldBe(bookStore.Name);
    }

    [Fact]
    public async Task GivenBookStoreDoesNotExists_GetBookStoreById_ShouldFail()
    {
        //Arrange
        var nonExistentBookStoreId = Guid.NewGuid();

        //Act
        var response = await testsFixture.Client.GetAsync($"book-stores/{nonExistentBookStoreId}");

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}