using ChatAPI.Application.Abstractions;
using ChatAPI.Application.Services;
using ChatAPI.Presenters;
using ChatAPI.Presenters.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers;



[ApiController]
[Route("[controller]")]

public class UserController : ApiController
{
    private readonly IUserService userService = new UserService();

    [HttpPost(Name = "Login")]
    public ActionResult<LoginResponse> Login(LoginRequest request)
    {
        var result = userService.LoginUser(UserContext, request.Email, request.Password);
        return Present(userService.LoginUser(UserContext, request.Email, request.Password), new LoginPresenter());
    }

}
