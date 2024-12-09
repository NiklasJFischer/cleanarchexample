using ChatAPI.Application.Abstractions.Repositories;

namespace ChatAPI.EntityFrameworkRepository
{
    public class EntityFrameworkUnitOfWork(ChatAPIDbContext dbContext) : IUnitOfWork
    {

        public void SaveChanges()
        {
            dbContext.SaveChangesAsync();
        }

        public ITransaction BeginTransaction()
        {
            return new EntityFrameworkTransaction(dbContext);
        }
    }
}
