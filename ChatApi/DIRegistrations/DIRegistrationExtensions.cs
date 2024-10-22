using ChatApi.ConsoleLogging;
using ChatAPI.Application.Abstractions;
using ChatAPI.Application.Abstractions.Providers;
using ChatAPI.Application.Abstractions.Repositories;
using ChatAPI.Application.Decorators;
using ChatAPI.DateTime;
using ChatAPI.EntityFrameworkRepository;
using ChatAPI.Hashing;
using ChatAPI.InMemoryRepository;
using ChatAPI.Tokens;
using Microsoft.EntityFrameworkCore;

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

        services.AddScoped<IUnitOfWork, EntityFrameworkUnitOfWork>();
        services.AddDbContext<ChatAPIDbContext>(
            options => options.UseInMemoryDatabase(databaseName: "MyDB")
        );
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
        services.Decorate(typeof(ICommandService<,>), typeof(UnitOfWorkDecorator<,>));
        services.Decorate(typeof(ICommandService<,>), typeof(UseCaseStatisticsDecorator<,>));
    }

    private static void AddApplicationProviders(this IServiceCollection services)
    {
        services.AddScoped<ITokenProvider, TokenProvider>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<ITokenProvider, TokenProvider>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<ConsoleLogger>();
        services.AddScoped<LogRepository>();
        services.AddScoped<ILogProvider, RegistrationCompositeLogProvider>();

        services.AddScoped<IHashProvider, HashProvider>();
        services.AddScoped<IHashProvider, HashProvider>();
    }
}
