using ChatAPI.Domain.Entities;

namespace ChatAPI.Application.Abstractions.Providers;

public interface ILogProvider
{
    public void AddLog(Log log);
}
