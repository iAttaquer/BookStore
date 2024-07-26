using System.Text.Json.Serialization;
using DotNetBoilerplate.Api.BookStores;
using DotNetBoilerplate.Api.Books;
using DotNetBoilerplate.Api.Reviews;
using DotNetBoilerplate.Api.Users;
using DotNetBoilerplate.Api.Catalogs;
using DotNetBoilerplate.Application;
using DotNetBoilerplate.Core;
using DotNetBoilerplate.Infrastructure;
using DotNetBoilerplate.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddShared()
    .AddApplication()
    .AddCore()
    .AddInfrastructure(builder.Configuration)
    .AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

var app = builder.Build();

app.MapUsersEndpoints();
app.MapBookStoresEndpoints();
app.MapBookEndpoints();
app.MapReviewEndpoints();
app.MapCatalogsEndpoints();

app.UseInfrastructure();

await app.RunAsync();

public partial class Program
{
}