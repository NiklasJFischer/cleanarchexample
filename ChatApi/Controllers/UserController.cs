using ChatApi.DTO;
using ChatAPI.Domain.Entities;
using ChatAPI.Domain.Enums;
using ChatAPI.InMemoryRepository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ChatApi.Controllers;



[ApiController]
[Route("[controller]")]

public class UserController : ApiController
{

    [HttpPost(Name = "Login")]
    public ActionResult<LoginResponse> Login(LoginRequest request)
    {
        string jwtToken = "";
        StatusCode statusCode = ChatAPI.Domain.Enums.StatusCode.Success;
        string resultMessage = "";

        try
        {

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                statusCode = ChatAPI.Domain.Enums.StatusCode.ValidationFailed;
                resultMessage = "Email is null";
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                statusCode = ChatAPI.Domain.Enums.StatusCode.ValidationFailed;
                resultMessage = "Password is null";
            }


            User? user = GetUserByEmail(request.Email);

            if (user == null)
            {
                statusCode = ChatAPI.Domain.Enums.StatusCode.ValidationFailed;
                resultMessage = "Invalid email";
            }
            else
            {
                string hash = GenerateHashByPasswordAndSalt(request.Password, user.PasswordSalt);

                if (!user.PasswordHash.Equals(hash))
                {
                    statusCode = ChatAPI.Domain.Enums.StatusCode.ValidationFailed;
                    resultMessage = "Invalid password";
                }

                jwtToken = CreateToken(user);
            }
        }
        catch (Exception ex)
        {
            var exLog = new Log() { Title = $"Login failed with unhandled error.", Description = ex.Message, Timestamp = DateTime.UtcNow };
            LogController.AddLog(exLog);
            AddLogToConsole(exLog);
            statusCode = ChatAPI.Domain.Enums.StatusCode.Error;
        }

        var log = new Log() { Title = $"Command Login executed.", Description = $"Authenticated: {UserContext.HasUserId}, StatusCode: {statusCode}", Timestamp = DateTime.UtcNow };
        LogController.AddLog(log);
        AddLogToConsole(log);


        return ToActionResult(statusCode, ToLoginDTO, jwtToken, resultMessage);
    }

    internal static string CreateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("MY_VERY_SECRET_AND_HARD_TO_GUESS_JWT_KEY_0123456789");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)

            ]),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = "MY_JWT_ISSUER",
            Audience = "MY_JWT_AUDIENCE"
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static LoginResponse ToLoginDTO(string result)
    {
        return new LoginResponse()
        {
            Jwt = result
        };
    }

    internal static string GenerateHashByPasswordAndSalt(string password, string salt)
    {
        var passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
        var saltBytes = Convert.FromBase64String(salt);
        byte[] passwordWithSaltBytes = [.. passwordBytes, .. saltBytes];
        string hash = Convert.ToBase64String(SHA256.HashData(passwordWithSaltBytes));
        return hash;
    }

    internal static void CreatePassword(string password, out string hash, out string salt)
    {
        var saltBytes = RandomNumberGenerator.GetBytes(32);
        salt = Convert.ToBase64String(saltBytes);
        hash = GenerateHashByPasswordAndSalt(password, salt);
    }

    internal static User? GetUserByEmail(string email)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(email, nameof(email));
        var matches = UserStorage.users.Where(u => u.Email.Equals(email));
        return matches.FirstOrDefault();
    }



    internal static IEnumerable<Guid> GetAllUserIds()
    {
        return UserStorage.users.Select(u => u.Id);
    }

    internal static User GetUserByIdInternal(Guid id)
    {
        var matches = UserStorage.users.Where(u => u.Id.Equals(id));

        if (!matches.Any())
        {
            throw new ArgumentException($"User with id {id} does not exist", nameof(id));
        }

        return matches.First();
    }

    internal static bool UserWithIdExists(Guid userId)
    {
        return UserStorage.users.Any(u => u.Id.Equals(userId));
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
