using ChatAPI.Application.Abstractions.UseCases;
using ChatAPI.Presenters;
using ChatAPI.Presenters.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController(ICreateMessageService createMessageService, IGetMessagesService getMessagesService) : ApiController
{

    [HttpGet(Name = "GetMessages")]
    public ActionResult<IEnumerable<MessageDTO>> Get()
    {
        return PresentEnumerable(getMessagesService.GetMessages(UserContext), new MessagePresenter());

    }

    [HttpPost(Name = "CreateMessage")]
    [Authorize()]
    public ActionResult<MessageDTO> CreateMessage(CreateMessageRequest request)
    {
        return Present(createMessageService.CreateMessage(UserContext, request.Text), new MessagePresenter());
    }

}
