using ChatAPI.Application.Abstractions.Repositories;

namespace ChatAPI.InMemoryRepository
{
    /// <summary>
    /// Dummy UnitOfWork => rollbacks not supported wih in memory storage
    /// </summary>
    public class InMemoryUnsupportedUnitOfWork : IUnitOfWork
    {
        public ITransaction BeginTransaction()
        {
            return new InMemoryNotSupportedTransaction();
        }

        public void SaveChanges()
        {

        }
    }
}
