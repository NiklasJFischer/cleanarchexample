using ChatAPI.Application.Abstractions.Providers;

namespace ChatAPI.DateTime;

public class DateTimeProvider : IDateTimeProvider
{
    public System.DateTime UtcNow => System.DateTime.UtcNow;
}
