
using ChatAPI.Application.Abstractions;
using ChatAPI.Application.Services;
using ChatAPI.Presenters;
using ChatAPI.Presenters.DTO;
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
        var result = messageService.GetMessages(UserContext);
        return PresentEnumerable(messageService.GetMessages(UserContext), new MessagePresenter());

    }

    [HttpPost(Name = "CreateMessage")]
    [Authorize()]
    public ActionResult<MessageDTO> CreateMessage(CreateMessageRequest request)
    {
        return Present(messageService.CreateMessage(UserContext, request.Text), new MessagePresenter());
    }

}
