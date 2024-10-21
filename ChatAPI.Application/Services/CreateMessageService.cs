using ChatAPI.Application.Abstractions;
using ChatAPI.Application.Abstractions.Providers;
using ChatAPI.Application.Abstractions.Repositories;
using ChatAPI.Application.Commands;
using ChatAPI.Application.Commands.Core;
using ChatAPI.Domain.Entities;

namespace ChatAPI.Application.Services;

public class CreateMessageService(IMessageRepository messageRepository, IDateTimeProvider dateTimeProvider, IUserRepository userRepository) : UseCase<Message>, ICommandService<CreateMessageCommand, Message>
{
    public CommandResult<Message> Execute(CreateMessageCommand command)
    {
        if (!command.IsLoggedIn)
        {
            return NotAuthenticated();
        }

        if (!userRepository.UserWithIdExists(command.UserContext.UserId))
        {
            return ValidationFailed($"User with id {command.UserContext.UserId} does not exist");
        }

        Message msg = new() { AuthorId = command.UserContext.UserId, Text = command.Text, Created = dateTimeProvider.UtcNow };
        var id = messageRepository.AddMessage(msg);

        Message? savedMsg = messageRepository.GetMessageById(id);

        if (savedMsg == null)
        {
            return Error("Message not created");
        }

        return Success(savedMsg);
    }
}
