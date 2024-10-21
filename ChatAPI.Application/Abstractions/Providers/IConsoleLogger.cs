using ChatAPI.Domain.Entities;

namespace ChatAPI.Application.Abstractions.Providers;

public interface IConsoleLogger
{
    public void AddLog(Log log);
}
