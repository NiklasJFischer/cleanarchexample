using ChatAPI.Application.Abstractions;
using ChatAPI.Application.Abstractions.Providers;
using ChatAPI.Application.Abstractions.Repositories;
using ChatAPI.Application.Commands.Core;
using ChatAPI.Domain.Entities;

namespace ChatAPI.Application.Decorators;

public class AuditLogDecorator<TCommand, TResult>(ICommandService<TCommand, TResult> decoratee, ILogProvider logProvider, IUserRepository userRepository, IDateTimeProvider dateTimeProvider) : ICommandService<TCommand, TResult> where TCommand : Command
{
    public CommandResult<TResult> Execute(TCommand command)
    {
        var result = decoratee.Execute(command);

        if (!command.IsLoggedIn)
        {
            return result;
        }

        if (result.StatusCode != CommandResultStatusCode.Success)
        {
            return result;
        }

        var user = userRepository.GetUserById(command.UserContext.UserId);
        if (user == null)
        {
            return result;
        }

        logProvider.AddLog(new Log() { Title = $"Command {command.GetType().Name} executed by {user.Name}.", Description = $"UserId: {user.Id}", Timestamp = dateTimeProvider.UtcNow });
        return result;
    }
}
