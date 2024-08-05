using System.Net;
using System.Net.Http.Json;
using DotNetBoilerplate.Api.Books;
using DotNetBoilerplate.Application.Books.DTO;
using DotNetBoilerplate.Api.Users;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using DotNetBoilerplate.Tests.Integration.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;
using DotNetBoilerplate.Api.BookStores;

namespace DotNetBoilerplate.Tests.Integration;

[Collection(nameof(BookEndpointsTestsCollection))]
public class BookEndpointsTests : IAsyncLifetime
{
    private readonly BookEndpointsTestsFixture testsFixture;

    public BookEndpointsTests(BookEndpointsTestsFixture testsFixture)
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
    private async Task<Guid> CreateBookStore()
    {
        var createBookStoreRequest = new CreateBookStoreEndpoint.Request
        {
            Name = "BookStore",
            Description = "Description"
        };
        var createBookStoreResponse = await testsFixture.Client.PostAsJsonAsync("book-stores", createBookStoreRequest);
        var createBookStoreResult = await createBookStoreResponse.Content.ReadFromJsonAsync<CreateBookStoreEndpoint.Response>();
        return createBookStoreResult!.Id;
    }
    [Fact]
    public async Task GivenBookStoreExist_CreateBook_ShouldSucceed()
    {
        //Arrange
        await CreateBookStore();
        var createBookRequest = new CreateBookEndpoint.Request
        {
            Title = "Book",
            Writer = "Writer",
            Genre = "Genre",
            Year = 2023,
            Description = "Description"
        };
        var createBookResponse = await testsFixture.Client.PostAsJsonAsync("books", createBookRequest);

        createBookResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        using var serviceScope = testsFixture.ServiceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<DotNetBoilerplateReadDbContext>();

        var book = await dbContext.Books.FirstOrDefaultAsync();

        book.ShouldNotBeNull();
        book!.Title.ShouldBe(createBookRequest.Title);
    }
    [Fact]
    public async Task GivenBookStoreDoesNotExist_CreateBook_ShouldFail()
    {
        var createBookRequest = new CreateBookEndpoint.Request
        {
            Title = "Book",
            Writer = "Writer",
            Genre = "Genre",
            Year = 2023,
            Description = "Description"
        };
        var createBookResponse = await testsFixture.Client.PostAsJsonAsync("books", createBookRequest);

        createBookResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
    [Fact]
    public async Task GivenMaxBooksExist_CreateBook_ShouldFail()
    {
        await CreateBookStore();
        var createBookRequest = new CreateBookEndpoint.Request
        {
            Title = "Book",
            Writer = "Writer",
            Genre = "Genre",
            Year = 2023,
            Description = "Description"
        };
        for (int i = 0; i<100; i++)
        {
            await testsFixture.Client.PostAsJsonAsync("books", createBookRequest);
        }
        var createBookResponse = await testsFixture.Client.PostAsJsonAsync("books", createBookRequest);
        createBookResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
    [Fact]
    public async Task GivenBookExist_UpdateBook_ShouldSucceed()
    {
        await CreateBookStore();
        var createBookRequest = new CreateBookEndpoint.Request
        {
            Title = "Book",
            Writer = "Writer",
            Genre = "Genre",
            Year = 2023,
            Description = "Description"
        };
        var createBookResponse = await testsFixture.Client.PostAsJsonAsync("books", createBookRequest);
        var createdBook = await createBookResponse.Content.ReadFromJsonAsync<CreateBookEndpoint.Response>();
        var updateBookRequest = new UpdateBookEndpoint.Request
        {
            Title = "Book2",
            Writer = "Writer",
            Genre = "Genre",
            Year = 2023,
            Description = "Description"
        };
        var updateBookResponse = await testsFixture.Client.PutAsJsonAsync($"books/{createdBook!.Id}", updateBookRequest);
        updateBookResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        using var serviceScope = testsFixture.ServiceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<DotNetBoilerplateReadDbContext>();

        var book = await dbContext.Books.FirstOrDefaultAsync(x => x.Id == createdBook.Id);
        book.Title.ShouldBe(updateBookRequest.Title);
    }
    [Fact]
    public async Task GivenBookDoesNotExist_UpdateBook_ShouldFail()
    {
        var nonExistentBookId = Guid.NewGuid();
        var updateBookRequest = new UpdateBookEndpoint.Request
        {
            Title = "Book2",
            Writer = "Writer",
            Genre = "Genre",
            Year = 2023,
            Description = "Description"
        };
        var updateBookResponse = await testsFixture.Client.PutAsJsonAsync($"books/{nonExistentBookId}", updateBookRequest);
        updateBookResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
    [Fact]
    public async Task GivenBooksExist_BrowseBooks_ShouldReturnAll()
    {
        await CreateBookStore();
        var createBookRequest = new CreateBookEndpoint.Request
        {
            Title = "Book",
            Writer = "Writer",
            Genre = "Genre",
            Year = 2023,
            Description = "Description"
        };
        await testsFixture.Client.PostAsJsonAsync("books", createBookRequest);
        await testsFixture.Client.PostAsJsonAsync("books", createBookRequest);

        var browseBooksResponse = await testsFixture.Client.GetAsync("books");
        browseBooksResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        var books = await browseBooksResponse.Content.ReadFromJsonAsync<IEnumerable<BookDto>>();
        books.Count().ShouldBe(2);
    }
    [Fact]
    public async Task GivenBooksExist_BrowseBooks_ShouldReturnBooksByBookStore()
    {
        var bookStoreId = await CreateBookStore();
        var createBookRequest = new CreateBookEndpoint.Request
        {
            Title = "Book",
            Writer = "Writer",
            Genre = "Genre",
            Year = 2023,
            Description = "Description"
        };
        await testsFixture.Client.PostAsJsonAsync("books", createBookRequest);
        await testsFixture.Client.PostAsJsonAsync("books", createBookRequest);

        var browseBooksResponse = await testsFixture.Client.GetAsync($"books?bookStoreId={bookStoreId!}");
        browseBooksResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        var books = await browseBooksResponse.Content.ReadFromJsonAsync<IEnumerable<BookDto>>();
        books.Count().ShouldBe(2);
    }
    [Fact]
    public async Task GivenBookExists_GetBookById_ShouldSucced()
    {
        await CreateBookStore();
        var createBookRequest = new CreateBookEndpoint.Request
        {
            Title = "Book",
            Writer = "Writer",
            Genre = "Genre",
            Year = 2023,
            Description = "Description"
        };
        var createBookResponse = await testsFixture.Client.PostAsJsonAsync("books", createBookRequest);
        var createdBook = await createBookResponse.Content.ReadFromJsonAsync<CreateBookEndpoint.Response>();

        var getBookByIdResponse = await testsFixture.Client.GetAsync($"books/{createdBook!.Id}");
        getBookByIdResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        var book = await getBookByIdResponse.Content.ReadFromJsonAsync<BookDto>();
        book.Title.ShouldBe(createBookRequest.Title);
    }
    [Fact]
    public async Task GivenBookDoesNotExist_GetBookById_ShouldFail()
    {
        var nonExistentBookId = Guid.NewGuid();

        var getBookByIdResponse = await testsFixture.Client.GetAsync($"books/{nonExistentBookId}");

        getBookByIdResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
    [Fact]
    public async Task GivenBookExists_DeleteBook_ShouldSucceed()
    {
        await CreateBookStore();
        var createBookRequest = new CreateBookEndpoint.Request
        {
            Title = "Book",
            Writer = "Writer",
            Genre = "Genre",
            Year = 2023,
            Description = "Description"
        };
        var createBookResponse = await testsFixture.Client.PostAsJsonAsync("books", createBookRequest);
        var createdBook = await createBookResponse.Content.ReadFromJsonAsync<CreateBookEndpoint.Response>();

        var deleteBookResponse = await testsFixture.Client.DeleteAsync($"books/{createdBook!.Id}");
        deleteBookResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent);

        using var serviceScope = testsFixture.ServiceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<DotNetBoilerplateReadDbContext>();

        var books = await dbContext.Books.ToListAsync();
        books.ShouldBeEmpty();
    }
    [Fact]
    public async Task GivenBookDoesNotExist_DeleteBook_ShouldFail()
    {
        var nonExistentBookId = Guid.NewGuid();

        var deleteBookResponse = await testsFixture.Client.DeleteAsync($"books/{nonExistentBookId}");

        deleteBookResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
}
