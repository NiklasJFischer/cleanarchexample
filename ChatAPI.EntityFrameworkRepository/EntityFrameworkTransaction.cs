using ChatAPI.Application.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ChatAPI.EntityFrameworkRepository
{
    public class EntityFrameworkTransaction(DbContext dbContext) : ITransaction
    {
        private readonly IDbContextTransaction dbContextTransaction = dbContext.Database.BeginTransaction();

        public void Commit() => dbContextTransaction?.Commit();

        public void Rollback() => dbContextTransaction?.Rollback();

        public void Dispose() => dbContextTransaction?.Dispose();
    }
}
