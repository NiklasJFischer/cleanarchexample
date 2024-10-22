namespace ChatAPI.Application.Abstractions.Repositories
{
    public interface IUnitOfWork
    {
        ITransaction BeginTransaction();
        void SaveChanges();
    }
}
