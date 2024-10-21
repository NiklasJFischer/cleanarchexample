using ChatApi.ConsoleLogging.Abstractions;
using ChatAPI.Application.Abstractions;
using ChatAPI.Application.Core;
using ChatAPI.DateTime.Abstractions;
using ChatAPI.Domain.Entities;
using ChatAPI.Domain.Enums;
using ChatAPI.InMemoryRepository.Abstractions;

namespace ChatAPI.Application.Services;

public class MessageService(IMessageRepository messageRepository, IDateTimeProvider dateTimeProvider, IUserRepository userRepository, ILogRepository logRepository, IConsoleLogger consoleLogger) : IMessageService
{

    public ServiceResult<Message> CreateMessage(UserContext userContext, string text)
    {
        Message msg = new() { AuthorId = userContext.UserId, Text = text, Created = dateTimeProvider.UtcNow };
        StatusCode statusCode = StatusCode.Success;
        string resultMessage = string.Empty;

        try
        {
            if (!userContext.HasUserId)
            {
                statusCode = StatusCode.NotAuthenticated;
            }

            if (statusCode == StatusCode.Success && !userRepository.UserWithIdExists(userContext.UserId))
            {
                statusCode = StatusCode.ValidationFailed;
                resultMessage = $"User with id {userContext.UserId} does not exist";
            }

            Guid id = messageRepository.AddMessage(msg);
            Message? savedMsg = messageRepository.GetMessageById(id);

            if (savedMsg == null)
            {
                statusCode = StatusCode.Error;
                resultMessage = "Message not created";
            }
            else
            {
                msg = savedMsg;
            }

            if (statusCode == StatusCode.Success)
            {
                var user = userRepository.GetUserById(userContext.UserId);
                if (user != null)
                {
                    msg.Author = user;
                    var auditLog = new Log() { Title = $"Command CreateMessage executed by {user.Name}.", Description = $"UserId: {user.Id}", Timestamp = dateTimeProvider.UtcNow };
                    logRepository.AddLog(auditLog);
                    consoleLogger.AddLog(auditLog);
                }
            }
        }
        catch (Exception ex)
        {
            var exLog = new Log() { Title = $"CreateMessage failed with unhandled error.", Description = ex.Message, Timestamp = dateTimeProvider.UtcNow };
            logRepository.AddLog(exLog);
            consoleLogger.AddLog(exLog);
            statusCode = StatusCode.Error;
            resultMessage = string.Empty;
        }

        var log = new Log() { Title = $"Command CreateMessage executed.", Description = $"Authenticated: {userContext.HasUserId}, StatusCode: {statusCode}", Timestamp = dateTimeProvider.UtcNow };
        logRepository.AddLog(log);
        consoleLogger.AddLog(log);


        return new ServiceResult<Message>(msg, statusCode, resultMessage);
    }

    public ServiceResult<IEnumerable<Message>> GetMessages(UserContext userContext)
    {
        IEnumerable<Message> allMessages = [];
        StatusCode statusCode = StatusCode.Success;

        try
        {

            allMessages = messageRepository.GetAllMessages();

            if (userContext.HasUserId)
            {
                var user = userRepository.GetUserById(userContext.UserId);
                if (user != null)
                {
                    var auditLog = new Log() { Title = $"Command GetMessages executed by {user.Name}.", Description = $"UserId: {user.Id}", Timestamp = dateTimeProvider.UtcNow };
                    logRepository.AddLog(auditLog);
                    consoleLogger.AddLog(auditLog);
                }
            }
        }
        catch (Exception ex)
        {
            var exLog = new Log() { Title = $"GetMessages failed with unhandled error.", Description = ex.Message, Timestamp = dateTimeProvider.UtcNow };
            logRepository.AddLog(exLog);
            consoleLogger.AddLog(exLog);
            statusCode = StatusCode.Error;
        }

        var log = new Log() { Title = $"Command GetMessages executed.", Description = $"Authenticated: {userContext.HasUserId}, StatusCode: {statusCode}", Timestamp = dateTimeProvider.UtcNow };
        logRepository.AddLog(log);
        consoleLogger.AddLog(log);


        return new ServiceResult<IEnumerable<Message>>(allMessages, statusCode, string.Empty);
    }
}
