using ChatAPI.Domain.Entities;

namespace ChatAPI.InMemoryRepository.Abstractions;

public interface IUserRepository
{
    public bool UserWithIdExists(Guid userId);

    public User? GetUserByEmail(string email);
    public User? GetUserById(Guid id);
}
