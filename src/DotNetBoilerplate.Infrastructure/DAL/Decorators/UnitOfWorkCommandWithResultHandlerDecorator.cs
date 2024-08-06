using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Infrastructure.DAL.Decorators;

internal sealed class
    UnitOfWorkCommandWithResultHandlerDecorator<TCommand, TResult>(
        ICommandHandler<TCommand, TResult> commandHandler,
        IUnitOfWork unitOfWork)
    : ICommandHandler<TCommand, TResult>
    where TCommand : class, ICommand
{
    public async Task<TResult> HandleAsync(TCommand command)
    {
        var result = await unitOfWork.ExecuteAsync(() => commandHandler.HandleAsync(command));
        return result;
    }
}