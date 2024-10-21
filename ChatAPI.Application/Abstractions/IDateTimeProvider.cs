namespace ChatAPI.Application.Abstractions;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
