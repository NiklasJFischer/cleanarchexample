using ChatAPI.Application.Abstractions;
using ChatAPI.Application.Services;
using ChatAPI.Domain.Entities;
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
        return ToActionResult(result.StatusCode, ToLoginDTO, result.Result, result.Message);
    }

    public static LoginResponse ToLoginDTO(string? jwt)
    {
        ArgumentNullException.ThrowIfNull(jwt, nameof(jwt));
        return new LoginResponse
        {
            Jwt = jwt
        };
    }

    public static UserDTO ToUserDTO(User user)
    {
        return new UserDTO
        {
            Id = user.Id.ToString(),
            Name = user.Name,
            Email = user.Email

        };
    }
}
