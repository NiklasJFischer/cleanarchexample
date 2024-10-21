namespace ChatAPI.Application.Core;

public class UserContext
{
    public Guid UserId { get; set; }

    public bool HasUserId { get { return UserId != Guid.Empty; } }
}
