using ChatAPI.Domain.Entities;

namespace ChatAPI.Application.Abstractions;

public interface IUserRepository
{
    public bool UserWithIdExists(Guid userId);

    public User? GetUserByEmail(string email);
    public User? GetUserById(Guid id);
}
