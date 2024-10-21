
using ChatAPI.Application.Abstractions;
using ChatAPI.Domain.Entities;
using ChatAPI.Presenters.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController(IMessageService messageService) : ApiController
{

    [HttpGet(Name = "GetMessages")]
    public ActionResult<IEnumerable<MessageDTO>> Get()
    {
        var result = messageService.GetMessages(UserContext);
        return ToActionResultEnumerable(result.StatusCode, ToMemberDTO, result.Result, result.Message);

    }

    [HttpPost(Name = "CreateMessage")]
    [Authorize()]
    public ActionResult<MessageDTO> CreateMessage(CreateMessageRequest request)
    {
        var result = messageService.CreateMessage(UserContext, request.Text);
        return ToActionResult(result.StatusCode, ToMemberDTO, result.Result, result.Message);
    }


    public static MessageDTO ToMemberDTO(Message? message)
    {
        ArgumentNullException.ThrowIfNull(message, nameof(message));
        return new MessageDTO
        {
            Id = message.Id.ToString(),
            Text = message.Text,
            Created = message.Created.ToString(),
            Author = UserController.ToUserDTO(message.Author)
        };
    }

}
