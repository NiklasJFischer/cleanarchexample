using ChatAPI.Domain.Entities;
using ChatAPI.Presenters.Core;
using ChatAPI.Presenters.DTO;

namespace ChatAPI.Presenters;

public class UserPresenter : IPresenter<UserDTO, User>
{
    public UserDTO Present(User user)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        return new UserDTO
        {
            Id = user.Id.ToString(),
            Name = user.Name,
            Email = user.Email
        };
    }
}
