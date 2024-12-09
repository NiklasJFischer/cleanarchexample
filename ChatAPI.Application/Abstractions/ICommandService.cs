using ChatAPI.Application.Commands.Core;

namespace ChatAPI.Application.Abstractions;

public interface ICommandService<TCommand, TResult> where TCommand : Command
{
    CommandResult<TResult> Execute(TCommand command);
}
