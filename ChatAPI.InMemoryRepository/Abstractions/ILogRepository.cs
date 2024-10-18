using ChatAPI.Domain.Entities;

namespace ChatAPI.InMemoryRepository.Abstractions;

public interface ILogRepository
{
    public void AddLog(Log log);
}
