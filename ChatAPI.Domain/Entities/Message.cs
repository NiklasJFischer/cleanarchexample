namespace ChatAPI.Domain.Entities;

public class Message : IEntity
{
    public Guid Id { get; set; }
    public string Text { get; set; } = default!;
    public DateTime Created { get; set; } = default!;
    public Guid AuthorId { get; set; }
    public User Author { get; set; } = default!;
}
