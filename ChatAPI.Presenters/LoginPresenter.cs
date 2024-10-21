using ChatAPI.Presenters.Core;
using ChatAPI.Presenters.DTO;

namespace ChatAPI.Presenters;

public class LoginPresenter : IPresenter<LoginResponse, string>
{
    public LoginResponse Present(string result)
    {
        return new LoginResponse()
        {
            Jwt = result
        };
    }
}
