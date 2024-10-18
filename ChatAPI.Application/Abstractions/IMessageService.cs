using ChatAPI.Application.Core;
using ChatAPI.Domain.Entities;

namespace ChatAPI.Application.Abstractions
{
    public interface IMessageService
    {
        ServiceResult<Message> CreateMessage(UserContext userContext, string text);
        ServiceResult<IEnumerable<Message>> GetMessages();
    }
}
