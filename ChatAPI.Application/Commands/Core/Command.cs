namespace ChatAPI.Application.Commands.Core;

public abstract class Command(UserContext userContext)
{
    public abstract bool IsWriteCommand { get; }

    public UserContext UserContext { get; set; } = userContext;

    public Command() : this(new UserContext()) { }

    public bool IsLoggedIn
    {
        get
        {
            return UserContext != null && UserContext.HasUserId;
        }
    }


}
