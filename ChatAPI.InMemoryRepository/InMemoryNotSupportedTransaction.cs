using ChatAPI.Application.Abstractions.Repositories;

namespace ChatAPI.InMemoryRepository
{
    /// <summary>
    /// Dummy Transaction => rollbacks not supported wih in memory storage
    /// </summary>
    public class InMemoryNotSupportedTransaction : ITransaction
    {
        public void Commit()
        {

        }

        public void Dispose()
        {

        }

        public void Rollback()
        {

        }
    }
}
