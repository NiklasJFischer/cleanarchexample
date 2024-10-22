using ChatApi.ConsoleLogging;
using ChatAPI.Application.Decorators;
using ChatAPI.InMemoryRepository;

namespace ChatApi.DIRegistrations
{
    public class RegistrationCompositeLogProvider : CompositeLogProvider
    {
        public RegistrationCompositeLogProvider(ConsoleLogger consoleLogger, LogRepository logRepository) : base([consoleLogger, logRepository]) { }
    }
}
