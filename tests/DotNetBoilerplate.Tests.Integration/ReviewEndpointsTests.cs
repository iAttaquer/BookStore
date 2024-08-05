using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using DotNetBoilerplate.Api.Reviews;
using DotNetBoilerplate.Api.BookStores;
using DotNetBoilerplate.Api.Books;
using DotNetBoilerplate.Application.Reviews.DTO;
using DotNetBoilerplate.Api.Users;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using DotNetBoilerplate.Tests.Integration.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace DotNetBoilerplate.Tests.Integration;

[Collection(nameof(ReviewEndpointsTestsCollection))]
public class ReviewEndpointsTests : IAsyncLifetime
{
    private readonly ReviewEndpointsTestsFixture testsFixture;

    public ReviewEndpointsTests(ReviewEndpointsTestsFixture testsFixture)
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


    private async Task<Guid> CreateBookStoreAndBook()
    {
        //create bookstore
        var createBookStoreRequest = new CreateBookStoreEndpoint.Request
        {
            Name = "BookStore",
            Description = "Description"
        };
        await testsFixture.Client.PostAsJsonAsync("book-stores", createBookStoreRequest);

        //create book
        var createBookRequest = new CreateBookEndpoint.Request
        {
            Title = "Tytul",
            Writer = "Autor",
            Genre = "Gatunek",
            Year = 2024,
            Description = "Opis"
        };
        var createBookResponse = await testsFixture.Client.PostAsJsonAsync("books", createBookRequest);
        var createBookResult = await createBookResponse.Content.ReadFromJsonAsync<CreateBookEndpoint.Response>();

        return createBookResult.Id;
    }


    [Fact]
    public async Task GivenReviewToThisBookDoesNotExist_CreateReview_ShouldSucceed()
    {
        //Arrange
        var book = await CreateBookStoreAndBook();

            //create review
        var createReviewRequest = new CreateReviewEndpoint.Request
        {
            Rating = 5,
            Comment = "Komenatrz"
        };

        //Act
        var createReviewResponse = await testsFixture.Client.PostAsJsonAsync($"reviews?bookId={book}", createReviewRequest);

        //Assert
        createReviewResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        using var serviceScope = testsFixture.ServiceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<DotNetBoilerplateReadDbContext>();

        var review = await dbContext.Reviews.FirstOrDefaultAsync();
        review.ShouldNotBeNull();
        review.Rating.ShouldBe(createReviewRequest.Rating);
    }

    [Fact]
    public async Task GivenReviewToThisBookAlreadyExists_CreateReview_ShouldFail()
    {
        //Arrange
        var book = await CreateBookStoreAndBook();

            //create first review
        var createFirstReviewRequest = new CreateReviewEndpoint.Request
        {
            Rating = 5,
            Comment = "Pierwszy komenatrz"
        };
            //create second review
        var createSecondReviewRequest = new CreateReviewEndpoint.Request
        {
            Rating = 7,
            Comment = "Drugi komenatrz"
        };

        //Act
        var createFirstReviewResponse = await testsFixture.Client.PostAsJsonAsync($"reviews?bookId={book}", createFirstReviewRequest);
        var createSecondReviewResponse = await testsFixture.Client.PostAsJsonAsync($"reviews?bookId={book}", createSecondReviewRequest);

        //Assert
        createFirstReviewResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        createSecondReviewResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenReviewExists_UpdateReviewAsUser_ShouldFail()
    {
        //Arrange
        var book = await CreateBookStoreAndBook();


            //create review
        var createReviewRequest = new CreateReviewEndpoint.Request
        {
            Rating = 5,
            Comment = "Komenatrz"
        };

        await testsFixture.Client.PostAsJsonAsync($"reviews?bookId={book}", createReviewRequest);

            //update review
        var updateReviewRequest = new UpdateReviewEndpoint.Request
        {
            Rating = 1,
            Comment = "Komenatrz"
        };

        //Act
        var updateReviewResponse = await testsFixture.Client.PutAsJsonAsync($"reviews?bookId={book}", updateReviewRequest);

        //Assert
        updateReviewResponse.StatusCode.ShouldBe(HttpStatusCode.MethodNotAllowed);
    }

    [Fact]
    public async Task GivenReviewExists_UpdateReviewAsAdmin_ShouldSucceed() /*in user core role.user() to role.admin()*/
    {
        //Arrange
        var book = await CreateBookStoreAndBook();


            //create review
        var createReviewRequest = new CreateReviewEndpoint.Request
        {
            Rating = 5,
            Comment = "Komenatrz"
        };

        var createReviewResponse = await testsFixture.Client.PostAsJsonAsync($"reviews?bookId={book}", createReviewRequest);
        var createReviewResult = await createReviewResponse.Content.ReadFromJsonAsync<CreateReviewEndpoint.Response>();

            //update review
        var updateReviewRequest = new UpdateReviewEndpoint.Request
        {
            Name = "name",
            Rating = 1,
            Comment = "Komenatrz"
        };

        //Act
        var updateReviewResponse = await testsFixture.Client.PutAsJsonAsync($"reviews/{createReviewResult.Id}", updateReviewRequest);

        //Assert
        updateReviewResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        using var serviceScope = testsFixture.ServiceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<DotNetBoilerplateReadDbContext>();
    
        var updatedReview = await dbContext.Reviews.FirstOrDefaultAsync();

        updatedReview.ShouldNotBeNull();
        updatedReview.Rating.ShouldBe(updateReviewRequest.Rating);
        updatedReview.Comment.ShouldBe(updateReviewRequest.Comment);
    }

    [Fact]
    public async Task GivenReviewDoesNotExist_UpdateReview_ShouldFail()
    {
        //Arrange
        var randomId = Guid.NewGuid();

        var updateReviewRequest = new UpdateReviewEndpoint.Request
        {
            Name = "name",
            Rating = 1,
            Comment = "Komenatrz"
        };

        //Act
        var updateReviewResponse = await testsFixture.Client.PutAsJsonAsync($"reviews/{randomId}", updateReviewRequest);

        //Assert
        updateReviewResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenReviewsExists_BrowseReviews_ShouldReturnAll()
    {
        //Arrange
        var book = await CreateBookStoreAndBook();


            //create first review
        var createFirstReviewRequest = new CreateReviewEndpoint.Request
        {
            Rating = 5,
            Comment = "Komenatrz"
        };

        await testsFixture.Client.PostAsJsonAsync($"reviews?bookId={book}", createFirstReviewRequest);

        await ReAuthorize("email2@t.pl", "ttttttt2", "username2");

            //create second review
        var createSecondReviewRequest = new CreateReviewEndpoint.Request
        {
            Rating = 2,
            Comment = "Komenatrz"
        };

        await testsFixture.Client.PostAsJsonAsync($"reviews?bookId={book}", createSecondReviewRequest);

        var browseReviewsResponse = await testsFixture.Client.GetAsync("reviews");
        browseReviewsResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        var content = await browseReviewsResponse.Content.ReadFromJsonAsync<IEnumerable<ReviewDto>>();
        content.Count().ShouldBe(2);
    }

    [Fact]
    public async Task GivenReviewsExistsForSpecificBook_BrowseReviews_ShouldReturnAll()
    {
       //Arrange
        var book = await CreateBookStoreAndBook();

            //create first review
        var createFirstReviewRequest = new CreateReviewEndpoint.Request
        {
            Rating = 5,
            Comment = "Komenatrz"
        };

        await testsFixture.Client.PostAsJsonAsync($"reviews?bookId={book}", createFirstReviewRequest);

        await ReAuthorize("email2@t.pl", "ttttttt2", "username2");

            //create second review
        var createSecondReviewRequest = new CreateReviewEndpoint.Request
        {
            Rating = 2,
            Comment = "Komenatrz"
        };

        await testsFixture.Client.PostAsJsonAsync($"reviews?bookId={book}", createSecondReviewRequest);

        var browseReviewsResponse = await testsFixture.Client.GetAsync($"reviews?bookId={book}");
        browseReviewsResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        var content = await browseReviewsResponse.Content.ReadFromJsonAsync<IEnumerable<ReviewDto>>();
        content.Count().ShouldBe(2);
    }

    [Fact] 
    public async Task GivenReviewExists_GetReviewById_ShouldSucceed()
    {
        //Arrange
        var book = await CreateBookStoreAndBook();


            //create review
        var createReviewRequest = new CreateReviewEndpoint.Request
        {
            Rating = 5,
            Comment = "Komenatrz"
        };

        var createReviewResponse = await testsFixture.Client.PostAsJsonAsync($"reviews?bookId={book}", createReviewRequest);
        var createReviewResult = await createReviewResponse.Content.ReadFromJsonAsync<CreateBookEndpoint.Response>();

        //Act
        var getReviewByIdResponse = await testsFixture.Client.GetAsync($"reviews/{createReviewResult.Id}");
        getReviewByIdResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        //Assert
        var content = await getReviewByIdResponse.Content.ReadFromJsonAsync<ReviewDto>();
        content.Rating.ShouldBe(createReviewRequest.Rating);
        content.Comment.ShouldBe(createReviewRequest.Comment);
    }

    [Fact]
    public async Task GivenReviewDoesNotExist_GetReviewById_ShouldFail()
    {
        //Arrange
        var randomId = Guid.NewGuid();

        //Act
        var getReviewByIdResponse = await testsFixture.Client.GetAsync($"reviews/{randomId}");

        //Assert
        getReviewByIdResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GivenReviewExists_DeleteReview_ShouldSucceed()
    {
        //Arrange
        var book = await CreateBookStoreAndBook();

            //create review
        var createReviewRequest = new CreateReviewEndpoint.Request
        {
            Rating = 5,
            Comment = "Komenatrz"
        };

        var createReviewResponse = await testsFixture.Client.PostAsJsonAsync($"reviews?bookId={book}", createReviewRequest);
        var createReviewResult = await createReviewResponse.Content.ReadFromJsonAsync<CreateBookEndpoint.Response>();

        //Act
        var review = await testsFixture.Client.DeleteAsync($"reviews/{createReviewResult.Id}");

        //Assert
        review.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task GivenReviewDoesNotExist_DeleteReview_ShouldFail(){
        var randomId = Guid.NewGuid();

        //Act
        var review = await testsFixture.Client.DeleteAsync($"reviews/{randomId}");

        //Assert
        review.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
}