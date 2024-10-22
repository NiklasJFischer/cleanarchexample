namespace ChatAPI.Application.Abstractions.Repositories
{
    public interface ITransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
