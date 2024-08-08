using System.Net;
using System.Net.Http.Json;
using DotNetBoilerplate.Api.BookStores;
using DotNetBoilerplate.Application.BookStores.DTO;
using DotNetBoilerplate.Api.Users;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using DotNetBoilerplate.Tests.Integration.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace DotNetBoilerplate.Tests.Integration;

[Collection(nameof(BookstoreEndpointsTestsCollection))]
public class BookStoreEndpointsTests : IAsyncLifetime
{
    private readonly BookstoreEndpointsTestsFixture testsFixture;

    public BookStoreEndpointsTests(BookstoreEndpointsTestsFixture testsFixture)
    {
        this.testsFixture = testsFixture;
    }

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
    private async Task ReAuthorize(string mail, string pass, string username)
    {
        testsFixture.Client.DefaultRequestHeaders.Remove("Authorization");
        await SignUpAndSignIn(mail, pass, username);
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
        var secondBookStore = new CreateBookStoreEndpoint.Request
        {
            Name = "BookStore2",
            Description = "Description2"
        };

        //Act
        var createFirstBookStoreResponse = await testsFixture.Client.PostAsJsonAsync("book-stores", existingBookStore);
        var createSecondBookStoreResponse = await testsFixture.Client.PostAsJsonAsync("book-stores", secondBookStore);

        //Assert
        createFirstBookStoreResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        createSecondBookStoreResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenBookStoreExists_UpdateBookStore_ShouldSucceed()
    {
        //Arrange
        var createBookStoreRequest = new CreateBookStoreEndpoint.Request
        {
            Name = "BookStore",
            Description = "Description"
        };
        var createBookStoreResponse = await testsFixture.Client.PostAsJsonAsync("book-stores", createBookStoreRequest);

        var result = await createBookStoreResponse.Content.ReadFromJsonAsync<CreateBookStoreEndpoint.Response>();

        var updateBookStoreRequest = new UpdateBookStoreEndpoint.Request
        {
            Name = "Updated book store",
            Description = "Updated description"
        };

        //Act
        var updateBookStoreResponse = await testsFixture.Client.PutAsJsonAsync($"book-stores/{result.Id}", updateBookStoreRequest);

         //Assert
        updateBookStoreResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        using var serviceScope = testsFixture.ServiceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<DotNetBoilerplateReadDbContext>();

        var updatedBookStore = await dbContext.BookStores.FirstOrDefaultAsync(x=>x.Id==result.Id);

        updatedBookStore.ShouldNotBeNull();
        updatedBookStore!.Name.ShouldBe(updateBookStoreRequest.Name);
    }

    [Fact]
    public async Task GivenBookStoreDoesNotExists_UpdateBookStore_ShouldFail()
    {
        //Arrange
        var updateBookStoreRequest = new UpdateBookStoreEndpoint.Request
        {
            Name = "Updated book store",
            Description = "Updated description"
        };

        var randomId = Guid.NewGuid();
        //Act
        var updateBookStoreResponse = await testsFixture.Client.PutAsJsonAsync($"book-stores/{randomId}", updateBookStoreRequest);

         //Assert
        updateBookStoreResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenBookStoresExist_BrowseBookStores_ShouldReturnAll()
    {
        //Arrange
        var createFirstBookStoreRequest = new CreateBookStoreEndpoint.Request
        {
            Name = "BookStore",
            Description = "Description"
        };

        var createSecondBookStoreRequest = new CreateBookStoreEndpoint.Request
        {
            Name = "BookStore2",
            Description = "Description2"
        };

        //Act
        await testsFixture.Client.PostAsJsonAsync("book-stores", createFirstBookStoreRequest);

        await ReAuthorize("email2@t.pl", "ttttttt2", "username2");

        await testsFixture.Client.PostAsJsonAsync("book-stores", createSecondBookStoreRequest);
        //Assert

        var browseBookStoresResponse = await testsFixture.Client.GetAsync("book-stores");

        browseBookStoresResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        var content = await browseBookStoresResponse.Content.ReadFromJsonAsync<IEnumerable<BookStoreDto>>();
        content.Count().ShouldBe(2);
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

        var createBookStoreResult = await response.Content.ReadFromJsonAsync<CreateBookStoreEndpoint.Response>();

        //Act
        var getBookStoreByIdResponse = await testsFixture.Client.GetAsync($"book-stores/{createBookStoreResult.Id}");

        //Assert
        getBookStoreByIdResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        var content = await getBookStoreByIdResponse.Content.ReadFromJsonAsync<BookStoreDto>();
        content.Name.ShouldBe(request.Name);
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