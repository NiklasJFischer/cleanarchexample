using ChatApi.ConsoleLogging;
using ChatApi.ConsoleLogging.Abstractions;
using ChatAPI.Application.Abstractions;
using ChatAPI.Application.Core;
using ChatAPI.DateTime;
using ChatAPI.DateTime.Abstractions;
using ChatAPI.Domain.Entities;
using ChatAPI.Domain.Enums;
using ChatAPI.InMemoryRepository;
using ChatAPI.InMemoryRepository.Abstractions;
using ChatAPI.InMemoryRepository.Data;

namespace ChatAPI.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository messageRepository = new MessageRepository();
        private readonly IDateTimeProvider dateTimeProvider = new DateTimeProvider();
        private readonly IUserRepository userRepository = new UserRepository();
        private readonly ILogRepository logRepository = new LogRepository();
        private readonly IConsoleLogger consoleLogger = new ConsoleLogger();

        public ServiceResult<Message> CreateMessage(UserContext userContext, string text)
        {
            ServiceResult<Message> result;

            try
            {
                if (!userContext.HasUserId)
                {
                    result = new ServiceResult<Message>(StatusCode.NotAuthenticated, "Not authenticated");
                }

                if (!userRepository.UserWithIdExists(userContext.UserId))
                {
                    result = new ServiceResult<Message>(StatusCode.ValidationFailed, $"User with id {userContext.UserId} does not exist");
                }

                Message msg = new() { Id = Guid.NewGuid(), AuthorId = userContext.UserId, Text = text, Created = dateTimeProvider.UtcNow };
                MessageStorage.messages.Add(msg);

                Message? savedMsg = messageRepository.GetMessageById(msg.Id);

                if (savedMsg == null)
                {
                    result = new ServiceResult<Message>(ChatAPI.Domain.Enums.StatusCode.Error, "Message not created");
                }

                var user = userRepository.GetUserById(userContext.UserId);
                if (user != null)
                {
                    msg.Author = user;
                    var auditLog = new Log() { Title = $"Command CreateMessage executed by {user.Name}.", Description = $"UserId: {user.Id}", Timestamp = DateTime.UtcNow };
                    logRepository.AddLog(auditLog);
                    consoleLogger.AddLog(auditLog);
                }
            }
            catch (Exception ex)
            {
                var exLog = new Log() { Title = $"CreateMessage failed with unhandled error.", Description = ex.Message, Timestamp = DateTime.UtcNow };
                logRepository.AddLog(exLog);
                consoleLogger.AddLog(exLog);
                statusCode = ChatAPI.Domain.Enums.StatusCode.Error;
            }

            var log = new Log() { Title = $"Command CreateMessage executed.", Description = $"Authenticated: {userContext.HasUserId}, StatusCode: {statusCode}", Timestamp = DateTime.UtcNow };
            logRepository.AddLog(log);
            consoleLogger.AddLog(log);


            return new ServiceResult<Message>(msg);
        }

        public ServiceResult<IEnumerable<Message>> GetMessages(UserContext userContext)
        {
            IEnumerable<Message> allMessages = [];
            StatusCode statusCode = ChatAPI.Domain.Enums.StatusCode.Success;

            try
            {

                allMessages = MessageStorage.messages.Select(m =>
                {
                    var user = userRepository.GetUserById(m.AuthorId);
                    if (user != null)
                    {
                        m.Author = user;
                    }
                    return m;
                });

                if (userContext.HasUserId)
                {
                    var user = userRepository.GetUserById(userContext.UserId);
                    if (user != null)
                    {
                        var auditLog = new Log() { Title = $"Command GetMessages executed by {user.Name}.", Description = $"UserId: {user.Id}", Timestamp = DateTime.UtcNow };
                        logRepository.AddLog(auditLog);
                        consoleLogger.AddLog(auditLog);
                    }
                }
            }
            catch (Exception ex)
            {
                var exLog = new Log() { Title = $"GetMessages failed with unhandled error.", Description = ex.Message, Timestamp = DateTime.UtcNow };
                logRepository.AddLog(exLog);
                consoleLogger.AddLog(exLog);
                statusCode = ChatAPI.Domain.Enums.StatusCode.Error;
            }

            var log = new Log() { Title = $"Command GetMessages executed.", Description = $"Authenticated: {userContext.HasUserId}, StatusCode: {statusCode}", Timestamp = DateTime.UtcNow };
            logRepository.AddLog(log);
            consoleLogger.AddLog(log);


            return new ServiceResult<IEnumerable<Message>>(allMessages);
        }
    }
}
