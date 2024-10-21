using ChatAPI.Application.Core;

namespace ChatAPI.Application.Abstractions.UseCases
{
    public interface ILoginUserService
    {
        ServiceResult<string> LoginUser(UserContext userContext, string email, string password);
    }
}
