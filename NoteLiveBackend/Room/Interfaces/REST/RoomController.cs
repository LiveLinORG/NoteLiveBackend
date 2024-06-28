using Microsoft.AspNetCore.Mvc;
using NoteLiveBackend.IAM.Interfaces.Transform;
using NoteLiveBackend.Room.Domain.Model.Commands;
using NoteLiveBackend.Room.Domain.Model.Queries;
using NoteLiveBackend.Room.Domain.Services;
using NoteLiveBackend.Room.Interfaces.REST.Resources;
using NoteLiveBackend.Room.Interfaces.REST.Transform;

namespace NoteLiveBackend.Room.Interfaces.REST;
[ApiController]
[Route("api/v1/[controller]")]
public class RoomController(IRoomCommandService roomCommandService,IRoomQueryService roomQueryServices) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomResource createRoomResource)
    {
        var createRoomCommand = CreateRoomCommandFromResourceAssembler.ToCommandFromResource(createRoomResource);
        var room = await roomCommandService.Handle(createRoomCommand);
        if (room is null) return BadRequest();
        var resource = RoomResourceFromEntityAssembler.ToResourceFromEntity(room);
        return CreatedAtAction(nameof(GetRoomById), new { roomId = resource.id }, resource);
    }

    [HttpGet("{roomId:guid}")]
    public async Task<IActionResult> GetRoomById([FromRoute]Guid roomId)
    {
        var room = await roomQueryServices.Handle(new GetRoomByIdQuery(roomId));
        if (room is null) return BadRequest();
        var resource = RoomResourceFromEntityAssembler.ToResourceFromEntity(room);
        return Ok(resource);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllRooms()
    {
        var getAllRoomsQuery = new GetAllRoomsQuery();
        var rooms = await roomQueryServices.Handle(getAllRoomsQuery);
        var resources = rooms.Select(RoomResourceFromEntityAssembler
            .ToResourceFromEntity); 
        return Ok(resources);
    }

    [HttpPost("{userId:guid}")]
    public async Task<IActionResult> AddUserToRoom([FromBody] AddUserToRoomResource addUserToRoomResource, [FromRoute] Guid userId)
    {

        var addUserToRoomCommand = AddUserToRoomCommandFromResourceAssembler.ToCommandFromResource(addUserToRoomResource, userId);
        var room = await roomCommandService.Handle(addUserToRoomCommand);
        return Ok(room);
    }

    [HttpPut("end/{roomId:guid}")]
    public async Task<IActionResult> EndRoomSession([FromRoute] Guid roomId)
    {
        var endRoomCommand = new EndRoomCommand(roomId);
        var room = await roomCommandService.Handle(endRoomCommand);
        return Ok(room);
    }
    [HttpPut("start/{roomId:guid}")]
    public async Task<IActionResult> StartRoomSession([FromRoute] Guid roomId)
    {
        var startRoomCommand = new StartRoomCommand(roomId);
        var room = await roomCommandService.Handle(startRoomCommand);
        return Ok(room);
    }
    [HttpPut("upload-pdf/{roomId:guid}")]
    public async Task<IActionResult> UploadPDFtoRoom([FromRoute] Guid roomId, [FromForm] IFormFile Content)
    {
        if (Content == null || Content.Length == 0)
        {
            return BadRequest(new { message = "No file uploaded." });
        }

        using (var memoryStream = new MemoryStream())
        {
            await Content.CopyToAsync(memoryStream);
            var uploadPDFCommand = new UploadPDFCommand(roomId, memoryStream.ToArray());
            var result = await roomCommandService.Handle(uploadPDFCommand);
            if (result)
                return Ok(new { message = "PDF uploaded successfully." });
            else
                return BadRequest(new { message = "Failed to upload PDF." });
        }
        
    }
    
    [HttpGet("byname/{roomName}")]
    public async Task<IActionResult> GetRoomByName([FromRoute] string roomName)
    {
        var room = await roomQueryServices.Handle(new GetRoomByNameQuery(roomName));
        if (room is null)
            return NotFound(); 
        var resource = RoomResourceFromEntityAssembler.ToResourceFromEntity(room);
        return Ok(resource);
    }

    // Otros métodos del controlador

    [HttpGet("{roomId:guid}/users")]
    public async Task<IActionResult> GetUsersByRoomId([FromRoute] Guid roomId)
    {
        var query = new GetUsersByRoomIdQuery(roomId);
        var users = await roomQueryServices.Handle(query);
        var resources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    
}
