using ChatAPI.Application.Abstractions;
using ChatAPI.Application.Abstractions.Providers;
using ChatAPI.Application.Commands.Core;
using ChatAPI.Domain.Entities;

namespace ChatAPI.Application.Decorators;

public class ExceptionHandlerDecorator<TCommand, TResult>(ICommandService<TCommand, TResult> decoratee, ILogProvider logProvider, IDateTimeProvider dateTimeProvider) : ICommandService<TCommand, TResult> where TCommand : Command
{
    public CommandResult<TResult> Execute(TCommand command)
    {
        try
        {
            return decoratee.Execute(command);
        }
        catch (Exception ex)
        {
            try
            {
                logProvider.AddLog(new Log() { Title = $"Command {command.GetType().Name} failed with unhandled error.", Description = ex.Message, Timestamp = dateTimeProvider.UtcNow });
            }
            catch { }

            return new CommandResult<TResult>(CommandResultStatusCode.Error, "Unhandled Exception");

        }
    }
}
