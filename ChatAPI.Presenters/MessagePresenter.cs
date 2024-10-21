using ChatAPI.Domain.Entities;
using ChatAPI.Presenters.Core;
using ChatAPI.Presenters.DTO;

namespace ChatAPI.Presenters;

public class MessagePresenter : IPresenter<MessageDTO, Message>
{
    public MessageDTO Present(Message message)
    {
        ArgumentNullException.ThrowIfNull(message, nameof(message));

        return new MessageDTO
        {
            Id = message.Id.ToString(),
            Text = message.Text,
            Created = message.Created.ToString(),
            Author = new UserPresenter().Present(message.Author),
        };
    }
}
