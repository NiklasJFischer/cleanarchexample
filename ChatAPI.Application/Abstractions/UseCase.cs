using ChatAPI.Application.Commands.Core;

namespace ChatAPI.Application.Abstractions;

public abstract class UseCase<TResult>
{

    public CommandResult<TResult> Success(TResult result)
    {
        return new CommandResult<TResult>(result);
    }
    public CommandResult<TResult> Error(string message)
    {
        return new CommandResult<TResult>(CommandResultStatusCode.Error, message);
    }
    public CommandResult<TResult> ValidationFailed(string message)
    {
        return new CommandResult<TResult>(CommandResultStatusCode.ValidationFailed, message);
    }
    public CommandResult<TResult> NotAuthenticated()
    {
        return new CommandResult<TResult>(CommandResultStatusCode.NotAuthenticated, "Not authenticated");
    }
    public CommandResult<TResult> NotAuthorized(string details)
    {
        return new CommandResult<TResult>(CommandResultStatusCode.NotAuthenticated, $"Not authorized, {details}");
    }
}
