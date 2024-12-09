using ChatAPI.Application.Abstractions.Repositories;

namespace ChatAPI.Test.UseCases.Stubs
{
    public class TestUnitOfWork : IUnitOfWork
    {
        public readonly TestTransaction Transaction = new TestTransaction();

        public ITransaction BeginTransaction()
        {
            return Transaction;
        }

        public void SaveChanges()
        {

        }
    }
}
