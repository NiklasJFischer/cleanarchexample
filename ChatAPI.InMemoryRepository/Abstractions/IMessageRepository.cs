using ChatAPI.Domain.Entities;

namespace ChatAPI.InMemoryRepository.Abstractions;

public interface IMessageRepository
{
    public IEnumerable<Message> GetAllMessages();

    public Guid AddMessage(Message message);
    public Message? GetMessageById(Guid id);
}
