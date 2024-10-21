using ChatAPI.Application.Core;
using ChatAPI.Domain.Entities;

namespace ChatAPI.Application.Abstractions.UseCases
{
    public interface IGetMessagesService
    {
        ServiceResult<IEnumerable<Message>> GetMessages(UserContext userContext);
    }
}
