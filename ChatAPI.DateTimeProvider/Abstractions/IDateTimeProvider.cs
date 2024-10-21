namespace ChatAPI.DateTime.Abstractions
{
    public interface IDateTimeProvider
    {
        System.DateTime UtcNow { get; }
    }
}
