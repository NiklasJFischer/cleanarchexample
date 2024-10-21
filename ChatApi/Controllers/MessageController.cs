
using ChatAPI.Application.Abstractions;
using ChatAPI.Presenters;
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
        return PresentEnumerable(messageService.GetMessages(UserContext), new MessagePresenter());

    }

    [HttpPost(Name = "CreateMessage")]
    [Authorize()]
    public ActionResult<MessageDTO> CreateMessage(CreateMessageRequest request)
    {
        return Present(messageService.CreateMessage(UserContext, request.Text), new MessagePresenter());
    }

}
