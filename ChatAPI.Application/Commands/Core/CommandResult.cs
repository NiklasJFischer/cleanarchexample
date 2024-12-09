namespace ChatAPI.Application.Commands.Core;

public class CommandResult<TResult>
{
    public TResult? Result { get; set; }
    public CommandResultStatusCode StatusCode { get; set; }

    public string Message { get; set; }

    public CommandResult(TResult result)
    {
        Result = result;
        StatusCode = CommandResultStatusCode.Success;
        Message = string.Empty;
    }

    public CommandResult(CommandResultStatusCode statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }
}
