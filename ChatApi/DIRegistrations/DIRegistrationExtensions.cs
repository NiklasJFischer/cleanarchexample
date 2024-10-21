using ChatApi.ConsoleLogging;
using ChatAPI.Application.Abstractions;
using ChatAPI.Application.Abstractions.Providers;
using ChatAPI.Application.Decorators;
using ChatAPI.DateTime;
using ChatAPI.Hashing;
using ChatAPI.InMemoryRepository;
using ChatAPI.Tokens;

namespace ChatApi.DIRegistrations;

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
        services.Scan(scan => scan
        .FromAssembliesOf(typeof(IRepository<>))
        .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
        .AsImplementedInterfaces()
        .WithScopedLifetime());
    }
    private static void AddApplicationServices(this IServiceCollection services)
    {
        services.Scan(scan => scan
        .FromAssembliesOf(typeof(ICommandService<,>))
        .AddClasses(classes => classes.AssignableTo(typeof(ICommandService<,>)))
        .AsImplementedInterfaces()
        .WithScopedLifetime());

        services.Decorate(typeof(ICommandService<,>), typeof(AuditLogDecorator<,>));
        services.Decorate(typeof(ICommandService<,>), typeof(ExceptionHandlerDecorator<,>));
        services.Decorate(typeof(ICommandService<,>), typeof(UseCaseStatisticsDecorator<,>));
    }

    private static void AddApplicationProviders(this IServiceCollection services)
    {
        services.AddScoped<ITokenProvider, TokenProvider>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<ITokenProvider, TokenProvider>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<ILogProvider>(x => new CompositeLogProvider(new ConsoleLogger(), new LogRepository()));
        services.AddScoped<IHashProvider, HashProvider>();
        services.AddScoped<IHashProvider, HashProvider>();
    }
}
