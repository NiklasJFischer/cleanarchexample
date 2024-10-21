using ChatAPI.Domain.Entities;

namespace ChatAPI.Application.Abstractions;

public interface IConsoleLogger
{
    public void AddLog(Log log);
}
