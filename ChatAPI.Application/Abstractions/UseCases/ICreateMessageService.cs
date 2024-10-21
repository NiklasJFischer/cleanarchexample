using ChatAPI.Application.Core;
using ChatAPI.Domain.Entities;

namespace ChatAPI.Application.Abstractions.UseCases
{
    public interface ICreateMessageService
    {
        ServiceResult<Message> CreateMessage(UserContext userContext, string text);
    }
}
