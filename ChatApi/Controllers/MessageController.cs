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
        ServiceResult<Message> msg = messageService.CreateMessage(UserContext, request.Text);
        return ToActionResult(msg.StatusCode, ToMemberDTO, msg.Result, msg.Message);
    }

    public static MessageDTO ToMemberDTO(Message? message)
    {
        if (message == null)
        {
            throw new ArgumentNullException(nameof(message));
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
