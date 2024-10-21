using ChatAPI.Application.Abstractions.Providers;
using ChatAPI.Domain.Entities;

namespace ChatAPI.Application.Decorators;

public class CompositeLogProvider(params ILogProvider[] logProviders) : ILogProvider
{
    private readonly ILogProvider[] logProviders = logProviders;

    public void AddLog(Log log)
    {
        foreach (var logProvider in logProviders)
        {
            logProvider.AddLog(log);
        }
    }
}
