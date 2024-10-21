using ChatAPI.Application.Abstractions.Providers;
using ChatAPI.Domain.Entities;
using ChatAPI.InMemoryRepository.Data;

namespace ChatAPI.InMemoryRepository;

public class LogRepository : ILogProvider, IRepository<Log>
{
    public void AddLog(Log log)
    {
        ArgumentNullException.ThrowIfNull(log, nameof(log));

        log.Id = Guid.NewGuid();
        LogStorage.logs.Add(log);
    }
}
