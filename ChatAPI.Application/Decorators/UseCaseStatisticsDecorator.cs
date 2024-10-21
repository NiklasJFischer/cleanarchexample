using ChatAPI.Application.Abstractions;
using ChatAPI.Application.Abstractions.Providers;
using ChatAPI.Application.Commands.Core;
using ChatAPI.Domain.Entities;

namespace ChatAPI.Application.Decorators;

public class UseCaseStatisticsDecorator<TCommand, TResult>(ICommandService<TCommand, TResult> decoratee, ILogProvider logProvider, IDateTimeProvider dateTimeProvider) : ICommandService<TCommand, TResult> where TCommand : Command
{
    public CommandResult<TResult> Execute(TCommand command)
    {
        var result = decoratee.Execute(command);

        logProvider.AddLog(new Log() { Title = $"Command {command.GetType().Name} executed.", Description = $"Authenticated: {command.IsLoggedIn}, StatusCode: {result.StatusCode}", Timestamp = dateTimeProvider.UtcNow });
        return result;
    }
}
