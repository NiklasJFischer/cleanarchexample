using ChatAPI.DateTime.Abstractions;

namespace ChatAPI.DateTime;

public class DateTimeProvider : IDateTimeProvider
{
    public System.DateTime UtcNow => System.DateTime.UtcNow;
}
