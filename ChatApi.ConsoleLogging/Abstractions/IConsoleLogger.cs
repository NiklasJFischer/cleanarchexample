using ChatAPI.Domain.Entities;

namespace ChatApi.ConsoleLogging.Abstractions;

public interface IConsoleLogger
{
    public void AddLog(Log log);
}
