using ChatAPI.Application.Abstractions.Providers;
using ChatAPI.Application.Abstractions.Repositories;
using ChatAPI.Application.Abstractions.UseCases;
using ChatAPI.Application.Core;
using ChatAPI.Domain.Entities;
using ChatAPI.Domain.Enums;

namespace ChatAPI.Application.Services
{
    public class GetMessagesService(IMessageRepository messageRepository, IDateTimeProvider dateTimeProvider, IUserRepository userRepository, ILogRepository logRepository, IConsoleLogger consoleLogger) : IGetMessagesService
    {
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
}
