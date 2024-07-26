using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Books.Delete;

public sealed record DeleteBookCommand(Guid bookId) : ICommand<Guid>;
