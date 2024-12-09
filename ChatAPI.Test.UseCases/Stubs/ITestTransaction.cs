using ChatAPI.Application.Abstractions.Repositories;

namespace ChatAPI.Test.UseCases.Stubs
{
    public class TestTransaction : ITransaction
    {
        public bool WasCommited { get; private set; }
        public bool WasRollbacked { get; private set; }

        public void Commit()
        {
            this.WasCommited = true;
        }

        public void Dispose()
        {
        }

        public void Rollback()
        {
            this.WasRollbacked = true;
        }
    }
}
