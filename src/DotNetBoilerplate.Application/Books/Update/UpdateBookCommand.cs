using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Books.Update;

public sealed record UpdateBookCommand(Guid Id, string Title, string Writer, 
    string Genre, int Year, string Description) : ICommand<Guid>;
