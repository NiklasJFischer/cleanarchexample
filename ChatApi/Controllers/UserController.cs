using ChatAPI.Application.Abstractions;
using ChatAPI.Application.Commands;
using ChatAPI.Presenters;
using ChatAPI.Presenters.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers;



[ApiController]
[Route("[controller]")]

public class UserController(ICommandService<LoginUserCommand, string> loginUserService) : ApiController
{

    [HttpPost(Name = "Login")]
    public ActionResult<LoginResponse> Login(LoginRequest request)
    {
        return Present(loginUserService.Execute(new LoginUserCommand(UserContext, request.Email, request.Password)), new LoginPresenter());
    }

}
