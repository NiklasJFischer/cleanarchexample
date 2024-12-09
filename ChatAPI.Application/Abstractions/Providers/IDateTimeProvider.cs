namespace ChatAPI.Application.Abstractions.Providers;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
