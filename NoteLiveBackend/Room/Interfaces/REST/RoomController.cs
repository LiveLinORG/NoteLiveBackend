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
    /**
     * <summary>
     *  Creates a new room.
     * </summary>
     * <param name="createRoomResource">The resource containing the details of the room to be created.</param>
     * <returns>A task</returns>
     */
    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomResource createRoomResource)
    {
        var createRoomCommand = CreateRoomCommandFromResourceAssembler.ToCommandFromResource(createRoomResource);
        var room = await roomCommandService.Handle(createRoomCommand);
        if (room is null) return BadRequest();
        var resource = RoomResourceFromEntityAssembler.ToResourceFromEntity(room);
        return CreatedAtAction(nameof(GetRoomById), new { roomId = resource.id }, resource);
    }
    
    /**
     * <summary>
     *  Gets a room by its identifier.
     * </summary>
     * <param name="roomId">The id of the room.</param>
     * <returns>A task</returns>
     */
    [HttpGet("{roomId:guid}")]
    public async Task<IActionResult> GetRoomById([FromRoute]Guid roomId)
    {
        var room = await roomQueryServices.Handle(new GetRoomByIdQuery(roomId));
        if (room is null) return BadRequest();
        var resource = RoomResourceFromEntityAssembler.ToResourceFromEntity(room);
        return Ok(resource);
    }
    
    /**
     * <summary>
     *  Gets all rooms
     * </summary>
     * <returns>A task</returns>
     */
    [HttpGet]
    public async Task<IActionResult> GetAllRooms()
    {
        var getAllRoomsQuery = new GetAllRoomsQuery();
        var rooms = await roomQueryServices.Handle(getAllRoomsQuery);
        var resources = rooms.Select(RoomResourceFromEntityAssembler
            .ToResourceFromEntity); 
        return Ok(resources);
    }

    /**
     * <summary>
     *  Adds a user to a room.
     * </summary>
     * <param name="addUserToRoomResource">he resource containing the details of the user to be added to the room.</param>
     * <returns>A task</returns>
     */
    [HttpPost("{userId:guid}")]
    public async Task<IActionResult> AddUserToRoom([FromBody] AddUserToRoomResource addUserToRoomResource, [FromRoute] Guid userId)
    {

        var addUserToRoomCommand = AddUserToRoomCommandFromResourceAssembler.ToCommandFromResource(addUserToRoomResource, userId);
        var room = await roomCommandService.Handle(addUserToRoomCommand);
        return Ok(room);
    }
    
    /**
     * <summary>
     *  Ends a room session.
     * </summary>
     * <param name="roomId">The id of da room.</param>
     * <returns>A task</returns>
     */
    [HttpPut("{roomId:guid}")]
    public async Task<IActionResult> EndRoomSession([FromRoute] Guid roomId)
    {
        var endRoomCommand = new EndRoomCommand(roomId);
        var room = await roomCommandService.Handle(endRoomCommand);
        return Ok(room);
    }
    
    /**
     * <summary>
     *  Uploads a PDF to a room.
     * </summary>
     * <param name="roomId">The id of da room.</param>
     * <param name="Content">The PDF file to be uploaded.</param>
     * <returns>A task</returns>
     */
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
    
    /**
     * <summary>
     *  Gets a room by its name.
     * </summary>
     * <param name="roomName">The name of the room.</param>
     * <returns>A task</returns>
     */
    [HttpGet("byname/{roomName}")]
    public async Task<IActionResult> GetRoomByName([FromRoute] string roomName)
    {
        var room = await roomQueryServices.Handle(new GetRoomByNameQuery(roomName));
        if (room is null)
            return NotFound(); 
        var resource = RoomResourceFromEntityAssembler.ToResourceFromEntity(room);
        return Ok(resource);
    }

    /**
     * <summary>
     *  Gets users by room identifier.
     * </summary>
     * <param name="roomId">The id of da room</param>
     * <returns>A task</returns>
     */
    [HttpGet("{roomId:guid}/users")]
    public async Task<IActionResult> GetUsersByRoomId([FromRoute] Guid roomId)
    {
        var query = new GetUsersByRoomIdQuery(roomId);
        var users = await roomQueryServices.Handle(query);
        var resources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    
}
