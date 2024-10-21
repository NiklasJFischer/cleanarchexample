using ChatAPI.Domain.Entities;
using ChatAPI.InMemoryRepository.Abstractions;
using ChatAPI.InMemoryRepository.Data;

namespace ChatAPI.InMemoryRepository;

public class UserRepository : IUserRepository
{
    public bool UserWithIdExists(Guid userId)
    {
        return UserStorage.users.Any(u => u.Id.Equals(userId));
    }

    public User? GetUserByEmail(string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));
        var matches = UserStorage.users.Where(u => u.Email.Equals(email));
        return matches.FirstOrDefault();
    }

    public User? GetUserById(Guid id)
    {
        var matches = UserStorage.users.Where(u => u.Id.Equals(id));
        return matches.FirstOrDefault();
    }
}
