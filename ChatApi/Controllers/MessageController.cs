using ChatAPI.Application.Abstractions;
using ChatAPI.Application.Commands;
using ChatAPI.Domain.Entities;
using ChatAPI.Presenters;
using ChatAPI.Presenters.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController(ICommandService<CreateMessageCommand, Message> createMessageService, ICommandService<GetAllMessagesCommand, IEnumerable<Message>> getMessagesService) : ApiController
{

    [HttpGet(Name = "GetMessages")]
    public ActionResult<IEnumerable<MessageDTO>> Get()
    {
        return PresentEnumerable(getMessagesService.Execute(new GetAllMessagesCommand(UserContext)), new MessagePresenter());

    }

    [HttpPost(Name = "CreateMessage")]
    [Authorize()]
    public ActionResult<MessageDTO> CreateMessage(CreateMessageRequest request)
    {
        return Present(createMessageService.Execute(new CreateMessageCommand(UserContext, request.Text)), new MessagePresenter());
    }

}
