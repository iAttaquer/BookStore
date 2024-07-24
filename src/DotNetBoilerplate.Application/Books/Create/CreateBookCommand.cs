using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Books.Create;

public sealed record CreateBookCommand(string Title, string Writer, 
    string Genre, int Year, string Description ) : ICommand<Guid>;
