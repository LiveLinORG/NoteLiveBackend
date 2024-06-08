using Microsoft.AspNetCore.Mvc;
using NoteLiveBackend.Room.Application.Internal.CommandServices;
using NoteLiveBackend.Room.Application.Internal.Queryservices;
using NoteLiveBackend.Room.Domain.Model.Commands;
using NoteLiveBackend.Room.Domain.Model.Queries;
using RoomCommands = NoteLiveBackend.Room.Domain.Model.Commands;

namespace NoteLiveBackend.Room.Interfaces.REST.Resources;
[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly CreateRoomCommandService _createRoomCommandService;
    private readonly GetRoomDetailsQueryService _getRoomDetailsQueryService;

    public RoomController(CreateRoomCommandService createRoomCommandService, GetRoomDetailsQueryService getRoomDetailsQueryService)
    {
        _createRoomCommandService = createRoomCommandService;
        _getRoomDetailsQueryService = getRoomDetailsQueryService;
    }

    [HttpPost]
    public IActionResult CreateRoom([FromBody] RoomCommands.CreateRoomCommand command)
    {
        _createRoomCommandService.Handle(command);
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetRoomDetails(Guid id)
    {
        var query = new GetRoomDetailsQuery(id);
        var room = _getRoomDetailsQueryService.Handle(query);
        return Ok(room);
    }
}
