﻿namespace ChatApi.Entities;

public class Message
{
    public Guid Id { get; set; }
    public string Text { get; set; } = default!;
    public DateTime Created { get; set; } = default!;
    public Guid AuthorId { get; set; }
    public User Author { get; set; } = default!;
}
