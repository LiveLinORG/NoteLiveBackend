using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NoteLiveBackend.Room.Domain.Model.Commands;
using NoteLiveBackend.Room.Domain.Services;
using NoteLiveBackend.Room.Interfaces.REST.Resources;
using NoteLiveBackend.Room.Interfaces.WebSocket;

namespace NoteLiveBackend.Room.Interfaces.REST;
[ApiController]
[Route("api/v1/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly IRoomCommandService _roomCommandService;

    public ChatController(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost("join")]
    public IActionResult JoinRoom([FromBody] JoinRoomRequest request)
    {
        return Ok(new { message = "Successfully joined the room." });
    }
    [HttpPost("check-activated")]
    public async Task<IActionResult> CheckIfActivated([FromBody] CheckIfActivatedRequest request)
    {
        var checkIfActivatedCommand = new CheckIfActivatedCommand(request.RoomId, request.UserId);
        var result = await _roomCommandService.Handle(checkIfActivatedCommand);
        if (result)
        {
            return Ok(new { message = "Room and Chat are active." });
        }
        else
        {
            return BadRequest(new { message = "Cannot join the room." });
        }
    }

}