using ChatAPI.Application.Core;

namespace ChatAPI.Application.Abstractions
{
    public interface IUserService
    {
        ServiceResult<string> LoginUser(string email, string password);
    }
}
