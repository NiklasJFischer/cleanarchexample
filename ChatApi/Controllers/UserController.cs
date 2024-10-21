using ChatAPI.Application.Abstractions;
using ChatAPI.Application.Services;
using ChatAPI.Presenters;
using ChatAPI.Presenters.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers;



[ApiController]
[Route("[controller]")]

public class UserController(IUserService userService) : ApiController
{

    [HttpPost(Name = "Login")]
    public ActionResult<LoginResponse> Login(LoginRequest request)
    {
        return Present(userService.LoginUser(UserContext, request.Email, request.Password), new LoginPresenter());
    }

}
