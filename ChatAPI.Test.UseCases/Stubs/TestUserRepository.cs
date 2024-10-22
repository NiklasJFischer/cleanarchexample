using ChatAPI.Application.Abstractions.Repositories;
using ChatAPI.Domain.Entities;

namespace ChatAPI.Test.UseCases.Stubs
{
    public class TestUserRepository : TestRepository<User>, IUserRepository
    {
        public User? GetUserByEmail(string email)
        {
            return GetByProperty(e => e.Email, email);
        }

        public User? GetUserById(Guid id)
        {
            return GetById(id);
        }

        public bool UserWithIdExists(Guid userId)
        {
            return ExistsById(userId);
        }
    }
}
