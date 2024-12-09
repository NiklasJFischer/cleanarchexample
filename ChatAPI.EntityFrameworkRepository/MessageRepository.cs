using ChatAPI.Application.Abstractions.Repositories;
using ChatAPI.Domain.Entities;
using ChatAPI.EntityFrameworkRepository;
using Microsoft.EntityFrameworkCore;

namespace ChatAPI.InMemoryRepository;

public class MessageRepository(ChatAPIDbContext dbContext) : IMessageRepository, IRepository<Message>
{
    public IEnumerable<Message> GetAllMessages()
    {
        return dbContext.Messages.Include(m => m.Author);
    }

    public Guid AddMessage(Message message)
    {

        ArgumentNullException.ThrowIfNull(message, nameof(message));

        message.Id = Guid.NewGuid();
        var a = dbContext.Messages.Add(message);
        return message.Id;
    }

    public Message? GetMessageById(Guid id)
    {
        return dbContext.Messages.Where(m => m.Id.Equals(id)).Include(m => m.Author).FirstOrDefault();
    }
}
