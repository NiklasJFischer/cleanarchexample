namespace ChatAPI.Application.Commands.Core;

public enum CommandResultStatusCode
{
    Success,
    Error,
    NotAuthenticated,
    NotAuthorized,
    ValidationFailed
}
