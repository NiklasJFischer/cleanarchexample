using ChatAPI.Application.Abstractions.Repositories;
using ChatAPI.Domain.Entities;

namespace ChatAPI.Test.UseCases.Stubs
{
    public class TestMessageRepository : TestRepository<Message>, IMessageRepository
    {

        public Guid AddMessage(Message message)
        {
            return Add(message);
        }

        public IEnumerable<Message> GetAllMessages()
        {
            return GetAll();
        }

        public Message? GetMessageById(Guid id)
        {
            return GetById(id);
        }
    }
}
