using ChatAPI.Application.Abstractions;
using ChatAPI.Application.Abstractions.Repositories;
using ChatAPI.Application.Commands;
using ChatAPI.Application.Commands.Core;
using ChatAPI.Domain.Entities;

namespace ChatAPI.Application.Services;

public class GetMessagesService(IMessageRepository messageRepository) : UseCase<IEnumerable<Message>>, ICommandService<GetAllMessagesCommand, IEnumerable<Message>>
{
    public CommandResult<IEnumerable<Message>> Execute(GetAllMessagesCommand command)
    {
        return Success(messageRepository.GetAllMessages());
    }
}
