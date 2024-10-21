using ChatAPI.Application.Core;

namespace ChatAPI.Application.Abstractions;

public interface IUserService
{
    ServiceResult<string> LoginUser(UserContext userContext, string email, string password);
}
