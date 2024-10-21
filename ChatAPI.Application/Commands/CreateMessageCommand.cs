using ChatAPI.Application.Commands.Core;

namespace ChatAPI.Application.Commands;

public class CreateMessageCommand(UserContext userContext, string text) : Command(userContext)
{
    public string Text { get; set; } = text;
}
