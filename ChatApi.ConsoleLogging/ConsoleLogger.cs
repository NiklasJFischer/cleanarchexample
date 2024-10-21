using ChatAPI.Application.Abstractions.Providers;
using ChatAPI.Domain.Entities;
using System.Diagnostics;

namespace ChatApi.ConsoleLogging;

public class ConsoleLogger : ILogProvider
{
    public void AddLog(Log log)
    {
        ArgumentNullException.ThrowIfNull(log, nameof(log));

        Debug.WriteLine("");
        Debug.WriteLine(log.Timestamp);
        Debug.WriteLine(log.Title);
        Debug.WriteLine(log.Description);
        Debug.WriteLine("");
    }
}
