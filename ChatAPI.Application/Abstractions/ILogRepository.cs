using ChatAPI.Domain.Entities;

namespace ChatAPI.Application.Abstractions;

public interface ILogRepository
{
    public void AddLog(Log log);
}
