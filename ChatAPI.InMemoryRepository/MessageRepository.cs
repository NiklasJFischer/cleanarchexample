using ChatAPI.Domain.Entities;
using ChatAPI.InMemoryRepository.Abstractions;
using ChatAPI.InMemoryRepository.Data;

namespace ChatAPI.InMemoryRepository;

public class MessageRepository : IMessageRepository
{
    public IEnumerable<Message> GetAllMessages()
    {
        return MessageStorage.messages.Select(m =>
        {
            m.Author = UserStorage.GetUserByIdInternal(m.AuthorId);
            return m;
        });
    }

    public Guid AddMessage(Message message)
    {

        ArgumentNullException.ThrowIfNull(message, nameof(message));

        message.Id = Guid.NewGuid();
        MessageStorage.messages.Add(message);
        return message.Id;
    }

    public Message? GetMessageById(Guid id)
    {
        var matches = MessageStorage.messages.Where(m => m.Id.Equals(id));

        if (!matches.Any())
        {
            return null;
        }

        return matches.Select(m =>
        {
            m.Author = UserStorage.GetUserByIdInternal(m.AuthorId);
            return m;
        }).First();


    }
}
