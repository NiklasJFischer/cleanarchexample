using ChatAPI.Application.Commands.Core;

namespace ChatAPI.Application.Commands;

public class LoginUserCommand(UserContext userContext, string email, string password) : Command(userContext)
{
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;

    public override bool IsWriteCommand => false;
}
