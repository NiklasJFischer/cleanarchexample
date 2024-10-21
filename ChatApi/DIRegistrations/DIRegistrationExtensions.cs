using ChatApi.ConsoleLogging;
using ChatApi.ConsoleLogging.Abstractions;
using ChatAPI.Application.Abstractions;
using ChatAPI.Application.Services;
using ChatAPI.DateTime;
using ChatAPI.DateTime.Abstractions;
using ChatAPI.Hashing;
using ChatAPI.Hashing.Abstractions;
using ChatAPI.InMemoryRepository;
using ChatAPI.InMemoryRepository.Abstractions;
using ChatAPI.Tokens;
using ChatAPI.Tokens.Abstractions;

namespace ChatApi.DIRegistrations
{
    public static class DIRegistrationExtensions
    {
        public static void AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddRepositories();
            services.AddApplicationServices();
            services.AddApplicationProviders();
        }

        private static void AddRepositories(this IServiceCollection services)
        {

            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
        }
        private static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IUserService, UserService>();
        }

        private static void AddApplicationProviders(this IServiceCollection services)
        {
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IConsoleLogger, ConsoleLogger>();
            services.AddScoped<IHashProvider, HashProvider>();
        }
    }
}
