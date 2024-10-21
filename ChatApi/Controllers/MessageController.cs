using ChatApi.DTO;
using ChatAPI.Application.Abstractions;
using ChatAPI.Application.Core;
using ChatAPI.Application.Services;
using ChatAPI.Domain.Entities;
using ChatAPI.Domain.Enums;
using ChatAPI.InMemoryRepository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ApiController
{

    private readonly IMessageService messageService = new MessageService();


    [HttpGet(Name = "GetMessages")]
    public ActionResult<IEnumerable<MessageDTO>> Get()
    {
        IEnumerable<Message> allMessages = [];
        StatusCode statusCode = ChatAPI.Domain.Enums.StatusCode.Success;

        try
        {
            allMessages = MessageStorage.messages.Select(m =>
            {
                m.Author = UserController.GetUserByIdInternal(m.AuthorId);
                return m;
            });

            if (UserContext.HasUserId)
            {
                var user = UserController.GetUserByIdInternal(UserContext.UserId);
                if (user != null)
                {
                    var auditLog = new Log() { Title = $"Command GetMessages executed by {user.Name}.", Description = $"UserId: {user.Id}", Timestamp = DateTime.UtcNow };
                    LogController.AddLog(auditLog);
                    AddLogToConsole(auditLog);
                }
            }
        }
        catch (Exception ex)
        {
            var exLog = new Log() { Title = $"GetMessages failed with unhandled error.", Description = ex.Message, Timestamp = DateTime.UtcNow };
            LogController.AddLog(exLog);
            AddLogToConsole(exLog);
            statusCode = ChatAPI.Domain.Enums.StatusCode.Error;
        }

        var log = new Log() { Title = $"Command GetMessages executed.", Description = $"Authenticated: {UserContext.HasUserId}, StatusCode: {statusCode}", Timestamp = DateTime.UtcNow };
        LogController.AddLog(log);
        AddLogToConsole(log);


        return ToActionResultEnumerable(statusCode, ToMemberDTO, allMessages, "");

    }

    [HttpPost(Name = "CreateMessage")]
    [Authorize()]
    public ActionResult<MessageDTO> CreateMessage(CreateMessageRequest request)
    {
        Message msg = new() { Id = Guid.NewGuid(), AuthorId = UserContext.UserId, Text = request.Text, Created = DateTime.UtcNow };
        StatusCode statusCode = Enums.StatusCode.Success;
        string resultMessage = "";

        try
        {
            if (!UserContext.HasUserId)
            {
                statusCode = Enums.StatusCode.NotAuthenticated;
            }
            }
            if (statusCode != Enums.StatusCode.Success && !UserController.UserWithIdExists(UserContext.UserId))
            {
                statusCode = Enums.StatusCode.ValidationFailed;
                resultMessage = $"User with id {UserContext.UserId} does not exist";
            }

            MessageStorage.messages.Add(msg);

            Message? savedMsg = GetMessageById(msg.Id);

            if (savedMsg == null)
            {
                statusCode = Enums.StatusCode.Error;
                resultMessage = "Message not created";
            }

            if (statusCode == Enums.StatusCode.Success)
            {
                var user = UserController.GetUserByIdInternal(UserContext.UserId);
                if (user != null)
                {
                    msg.Author = UserController.GetUserByIdInternal(UserContext.UserId);
                    var auditLog = new Log() { Title = $"Command CreateMessage executed by {user.Name}.", Description = $"UserId: {user.Id}", Timestamp = DateTime.UtcNow };
                    LogController.AddLog(auditLog);
                    AddLogToConsole(auditLog);
                }
            }
        }
        catch (Exception ex)
        {
            var exLog = new Log() { Title = $"CreateMessage failed with unhandled error.", Description = ex.Message, Timestamp = DateTime.UtcNow };
            LogController.AddLog(exLog);
            AddLogToConsole(exLog);
            statusCode = Enums.StatusCode.Error;
            statusCode = Enums.StatusCode.Error;
        }

        return new MessageDTO
        {
            Id = message.Id.ToString(),
            Text = message.Text,
            Created = message.Created.ToString(),
            Author = UserController.ToUserDTO(message.Author)
        };
    }




}
