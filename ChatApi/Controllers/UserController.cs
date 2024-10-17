using ChatApi.Data;
using ChatApi.DTO;
using ChatApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers;



[ApiController]
[Route("[controller]")]

public class UserController : ApiController
{



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
