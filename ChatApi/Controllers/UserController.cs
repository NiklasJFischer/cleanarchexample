using ChatAPI.Application.Abstractions.UseCases;
using ChatAPI.Presenters;
using ChatAPI.Presenters.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers;



[ApiController]
[Route("[controller]")]

public class UserController(ILoginUserService loginUserService) : ApiController
{

    [HttpPost(Name = "Login")]
    public ActionResult<LoginResponse> Login(LoginRequest request)
    {
        return Present(loginUserService.LoginUser(UserContext, request.Email, request.Password), new LoginPresenter());
    }

}
