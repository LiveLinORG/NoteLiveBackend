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
    
    /**
     * <summary>
     *  Initializes a new instance of the "ChatController" class
     * </summary>
     * <param name="hubContext">The SignalR hub context for chat.</param>
     */
    public ChatController(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }
    
    /**
     * <summary>
     *  Joins a user to a room.
     * </summary>
     * <param name="request">The request containing room joining information.</param>
     * <returns>An IActionResult representing the result of the operation.</returns>
     */
    [HttpPost("join")]
    public IActionResult JoinRoom([FromBody] JoinRoomRequest request)
    {
        return Ok(new { message = "Successfully joined the room." });
    }
    
    /**
     * <summary>
     *  Checks if a room and chat are activated.
     * </summary>
     * <param name="request">The request containing the room and user identifiers.</param>
     * <returns>A task</returns>
     */
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