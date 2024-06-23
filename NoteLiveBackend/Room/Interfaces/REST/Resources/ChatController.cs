using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NoteLiveBackend.Room.Application.Internal.Queryservices;
using NoteLiveBackend.Room.Domain.Model.Queries;
using NoteLiveBackend.Room.Interfaces.WebSocket;

namespace NoteLiveBackend.Room.Interfaces.REST.Resources;
[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IHubContext<ChatHub> _hubContext;

    public ChatController(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost("join")]
    public IActionResult JoinRoom([FromBody] JoinRoomRequest request)
    {


        return Ok(new { message = "Successfully joined the room." });
    }

    public class JoinRoomRequest
    {
        public string RoomId { get; set; }
        public string UserId { get; set; }
    }
}