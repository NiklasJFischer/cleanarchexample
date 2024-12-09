using ChatAPI.Application.Abstractions.Providers;
using ChatAPI.Domain.Entities;
using ChatAPI.EntityFrameworkRepository;

namespace ChatAPI.InMemoryRepository;

public class LogRepository(ChatAPIDbContext dbContext) : ILogProvider, IRepository<Log>
{
    public void AddLog(Log log)
    {
        dbContext.Add(log);
    }
}
