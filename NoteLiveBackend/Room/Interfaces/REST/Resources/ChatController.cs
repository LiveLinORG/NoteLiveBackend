using Microsoft.AspNetCore.Mvc;
using NoteLiveBackend.Room.Domain.Model.Queries;

namespace NoteLiveBackend.Room.Interfaces.REST.Resources;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly GetChatMessagesQueryService _getChatMessagesQueryService;

    public ChatController(GetChatMessagesQueryService getChatMessagesQueryService)
    {
        _getChatMessagesQueryService = getChatMessagesQueryService;
    }

    [HttpGet("{roomId}")]
    public IActionResult GetChatMessages(Guid roomId)
    {
        var query = new GetChatMessagesQuery(roomId);
        var messages = _getChatMessagesQueryService.Handle(query);
        return Ok(messages);
    }
}
