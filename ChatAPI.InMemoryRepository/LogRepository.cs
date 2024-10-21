using ChatAPI.Application.Abstractions;
using ChatAPI.Domain.Entities;
using ChatAPI.InMemoryRepository.Data;

namespace ChatAPI.InMemoryRepository;

public class LogRepository : ILogRepository
{
    public void AddLog(Log log)
    {
        ArgumentNullException.ThrowIfNull(log, nameof(log));

        log.Id = Guid.NewGuid();
        LogStorage.logs.Add(log);
    }
}
