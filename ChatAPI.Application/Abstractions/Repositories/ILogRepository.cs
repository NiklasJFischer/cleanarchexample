using ChatAPI.Domain.Entities;

namespace ChatAPI.Application.Abstractions.Repositories;

public interface ILogRepository
{
    public void AddLog(Log log);
}
