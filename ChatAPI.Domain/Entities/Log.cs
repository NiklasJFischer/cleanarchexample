namespace ChatAPI.Domain.Entities;

public class Log
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime Timestamp { get; set; }
}
